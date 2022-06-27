using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xamarin_app.Query;
using static Android.Support.V4.View.ViewPager;
using static Android.Views.View;
using AlertDialog = Android.Support.V7.App.AlertDialog;

/// <summary>
/// LearnActivity, which is used to go through the vocabulary of a category and listen to the sounds.
/// </summary>
namespace xamarin_app.Droid
{
    [Activity(Label = "LearnActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LearnActivity : Activity, IOnClickListener, IOnPageChangeListener
    {
        private ViewPager vocabularyPager;
        private List<VocabularyItem> vocabularyItems;
        private ImageButton nextButton, previousButton;
        private int categoryID;
        private Learn lernen;
        private long LastButtonClickTime;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B3_vocabulary_navigation);
            categoryID = Intent.GetIntExtra("key", -1);
            if (categoryID == -1)
            {
                Finish();
            }

            vocabularyItems = new List<VocabularyItem>();
            lernen = new Learn(categoryID);

            bool add;
            while (lernen.HasNext())
            {
                add = true;
                try
                {
                    int test = (int)typeof(Resource.Drawable).GetField(lernen.GetPicture()).GetValue(null);
                    test = (int)typeof(Resource.Raw).GetField(lernen.GetAudio()).GetValue(null);
                }
                catch
                {
                    add = false;
                }
                if (add) AddVocabularyItem();
                lernen.Next();
                if (!lernen.HasNext())
                {
                    add = true;
                    try
                    {
                        int test = (int)typeof(Resource.Drawable).GetField(lernen.GetPicture()).GetValue(null);
                        test = (int)typeof(Resource.Raw).GetField(lernen.GetAudio()).GetValue(null);
                    }
                    catch
                    {
                        add = false;
                    }
                    if (add) AddVocabularyItem();
                }
            }



            vocabularyPager = FindViewById<ViewPager>(Resource.Id.vocabularyPager);
            ViewPageAdapterLearn adapter = new ViewPageAdapterLearn(this, vocabularyItems);
            vocabularyPager.Adapter = adapter;

            previousButton = FindViewById<ImageButton>(Resource.Id.previousButton);
            previousButton.SetOnClickListener(this);

            nextButton = FindViewById<ImageButton>(Resource.Id.nextButton);
            nextButton.SetOnClickListener(this);

            nextButton.Visibility = Android.Views.ViewStates.Visible;
            vocabularyPager.AddOnPageChangeListener(this);

            FindViewById<TextView>(Resource.Id.currentNumber).Text = (vocabularyPager.CurrentItem + 1).ToString();
            FindViewById<TextView>(Resource.Id.maxNumber).Text = vocabularyItems.Count().ToString();

        }

        protected override void OnResume()
        {
            base.OnResume();
            LastButtonClickTime = 0;
        }
        private void AddVocabularyItem()
        {
            StringBuilder sb = new StringBuilder();
            VocabularyItem vocabularyItem = new VocabularyItem();

            vocabularyItem.VocabularyTextBavarian = lernen.GetVocab();
            vocabularyItem.VocabularyTextGerman = lernen.GetStandardGerman();
            vocabularyItem.VocabularyTextUserLanguage = lernen.GetTranslation();


            if (lernen.GetComment().Equals("") && lernen.GetWord().Equals(""))
            {
                sb.Append("");
            }
            else
            {
                sb.Append("Bemerkung:");
                if (!lernen.GetComment().Equals(""))
                {

                    sb.Append("\n");
                    sb.Append(lernen.GetComment());
                }
                else
                {
                    sb.Append("\n");
                    sb.Append(lernen.GetWord());
                }
            }
            vocabularyItem.VocabularyPopUpText = sb.ToString();
            sb.Clear();

            vocabularyItem.VocabularyImage = lernen.GetPicture();
            vocabularyItem.VocabularyAudio = lernen.GetAudio();

            vocabularyItems.Add(vocabularyItem);
        }

        /// <summary>
        /// When a button in a view is clicked, this method handles where to navigate.
        /// </summary>
        /// <param name="v"></param>
        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.previousButton:
                    {
                        if (vocabularyPager.CurrentItem > 0)
                        {
                            vocabularyPager.SetCurrentItem(vocabularyPager.CurrentItem - 1, true);
                            if (vocabularyPager.CurrentItem == 0)
                            {
                                previousButton.Visibility = Android.Views.ViewStates.Invisible;
                            }

                        }
                    }
                    break;
                case Resource.Id.nextButton:
                    {

                        if (vocabularyPager.CurrentItem < vocabularyItems.Count - 1)
                        {
                            vocabularyPager.SetCurrentItem(vocabularyPager.CurrentItem + 1, true);
                        }
                        else
                        {
                            if (SystemClock.ElapsedRealtime() - LastButtonClickTime < 1000)
                            {
                                return;
                            }

                            LastButtonClickTime = SystemClock.ElapsedRealtime();
                            lernen.SetExecution();
                            Finish();
                            Intent intent = new Intent(this, typeof(CongratulationsActivity));
                            intent.PutExtra("from", "LearnActivity");
                            intent.PutExtra("categoryID", categoryID);
                            StartActivity(intent);
                        }
                        break;
                    }
            }
        }

        public void OnPageScrollStateChanged(int state)
        {

        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        /// <summary>
        /// Navigates thorugh the vocabulary
        /// </summary>
        /// <param name="position">current position</param>
        public void OnPageSelected(int position)
        {
            FindViewById<TextView>(Resource.Id.currentNumber).Text = (vocabularyPager.CurrentItem + 1).ToString();
            FindViewById<TextView>(Resource.Id.maxNumber).Text = vocabularyItems.Count().ToString();

            Animation anim = new AlphaAnimation(0.2f, 1.0f);
            if (vocabularyPager.CurrentItem < vocabularyItems.Count - 1)
            {

                nextButton.ClearAnimation();
                nextButton.SetImageResource(Resource.Drawable.nextButton_blank);
            }
            else
            {
                nextButton.SetImageResource(Resource.Drawable.finishButton_blank);
                anim.Duration = 400;
                anim.StartOffset = 20;
                anim.RepeatMode = RepeatMode.Reverse;
                anim.RepeatCount = 50;
                nextButton.StartAnimation(anim);
            }
            if (vocabularyPager.CurrentItem > 0)
            {
                previousButton.Visibility = Android.Views.ViewStates.Visible;

            }
            else
            {
                previousButton.Visibility = Android.Views.ViewStates.Invisible;
            }
        }

        /// <summary>
        /// Opens a Dialog when the native android backbutton is pressed, before leaving the learnactivity
        /// </summary>
        public override void OnBackPressed()
        {
            AlertDialog.Builder builder
            = new AlertDialog
                  .Builder(this);

            builder.SetTitle("Wirklich zurück ins Hauptmenü?");
            builder.SetMessage("Der Fortschritt wird zurückgesetzt!");
            builder.SetCancelable(false);
            builder.SetPositiveButton("Ja", YesAction);
            builder.SetNegativeButton("Nein", NoAction);
            builder.Create().Show();

        }

        private void YesAction(object sender, DialogClickEventArgs e)
        {

            Finish();
        }
        private void NoAction(object sender, DialogClickEventArgs e)
        {
            //bleibe hier.
        }

        public void enableNavigation()
        {

        }
        public void disableNavigation()
        {

        }
    }
}
