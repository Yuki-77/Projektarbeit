using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using xamarin_app.DataBaseClasses;
using xamarin_app.Logic;
using xamarin_app.Query;
using static Android.Support.V4.View.ViewPager;
using static Android.Views.View;

/// <summary>
/// The Activity for Testing
/// </summary>
namespace xamarin_app.Droid
{
    [Activity(Label = "AbfragenActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    class TestActivity : Activity, IOnClickListener, IOnPageChangeListener
    {
        public static ImageButton nextButton;
        private static int categoryID;
        public static NonSwipeableViewPager vocabularyPager;
        private static List<TestVocabs> vocabularyItems;
        public static int rightItems;
        public static Animation anim;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B4_Navigation);
            categoryID = Intent.GetIntExtra("key", -1);
            if (categoryID == -1)
            {
                Finish();
            }

            rightItems = 0;
            List<Bairisch> validItems = GetAll();
            vocabularyItems = Test.GetAllItems(validItems);

            vocabularyPager = FindViewById<NonSwipeableViewPager>(Resource.Id.vocabularyPager);
            vocabularyPager.SetPagingEnabled(false);
            ViewPageAdapterTest adapter = new ViewPageAdapterTest(this, vocabularyItems);
            vocabularyPager.Adapter = adapter;

            nextButton = FindViewById<ImageButton>(Resource.Id.nextButton);
            nextButton.Visibility = ViewStates.Invisible;
            nextButton.SetOnClickListener(this);

            vocabularyPager.AddOnPageChangeListener(this);

            FindViewById<TextView>(Resource.Id.currentNumber).Text = (vocabularyPager.CurrentItem + 1).ToString();
            FindViewById<TextView>(Resource.Id.maxNumber).Text = vocabularyItems.Count().ToString();

        }

        private List<Bairisch> GetAll()
        {
            List<Bairisch> res = new List<Bairisch>();
            Test test = new Test(categoryID);
            bool add;

            while (test.HasNext())
            {
                add = true;
                try
                {
                    int exists = (int)typeof(Resource.Drawable).GetField(test.GetPicture()).GetValue(null);
                    exists = (int)typeof(Resource.Raw).GetField(test.GetAudio()).GetValue(null);
                }
                catch
                {
                    add = false;
                }
                if (add)
                {
                    res.Add(test.GetActualObject());
                }
                test.Next();
            }
            add = true;
            try
            {
                int exists = (int)typeof(Resource.Drawable).GetField(test.GetPicture()).GetValue(null);
                exists = (int)typeof(Resource.Raw).GetField(test.GetAudio()).GetValue(null);
            }
            catch
            {
                add = false;
            }
            if (add)
                res.Add(test.GetActualObject());
            return res;
        }

        /// <summary>
        /// When a button in a view is clicked, this method handles where to navigate.
        /// </summary>
        /// <param name="v"></param>
        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.nextButton:
                    {
                        if (vocabularyPager.CurrentItem < vocabularyItems.Count - 1)
                        {
                            vocabularyPager.SetPagingEnabled(false);
                            vocabularyPager.SetCurrentItem(vocabularyPager.CurrentItem + 1, true);
                        }
                        else
                        {
                            Finish();
                            Test.SetExecution(categoryID);
                            Intent intent = new Intent(this, typeof(CongratulationsActivity));
                            intent.PutExtra("from", "AbfragenActivity");
                            intent.PutExtra("categoryID", categoryID);
                            intent.PutExtra("correctItems", rightItems);
                            intent.PutExtra("totalItems", vocabularyItems.Count);
                            StartActivity(intent);
                        }
                    }
                    break;
            }
        }

        public void OnPageScrollStateChanged(int state)
        {
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        /// <summary>
        /// navigates thorugh the vocabulary
        /// </summary>
        /// <param name="position">current position</param>
        public void OnPageSelected(int position)
        {
            nextButton.Visibility = ViewStates.Invisible;
            vocabularyPager.SetPagingEnabled(false);

            FindViewById<TextView>(Resource.Id.currentNumber).Text = (vocabularyPager.CurrentItem + 1).ToString();
            FindViewById<TextView>(Resource.Id.maxNumber).Text = vocabularyItems.Count().ToString();

            anim = new AlphaAnimation(0.2f, 1.0f);
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

        }

        public void EnableNavigation()
        {

        }
        public void DisableNavigation()
        {

        }
    }
}