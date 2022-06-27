using Android.Content;
using Android.Media;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using static Android.Media.MediaPlayer;

namespace xamarin_app.Droid
{
    /// <summary>
    /// The ViewPager, which fills the views of each vocabularyItem from a list, which are then displayed in the LearnActivity.
    /// </summary>
    class ViewPageAdapterLearn : PagerAdapter, IOnCompletionListener
    {

        LearnActivity activity;
        private List<VocabularyItem> vocabularyItems;
        private MediaPlayer player;
        private LayoutInflater inflater;
        private bool infoPressed;
        ImageButton playButton;

        /// <summary>
        /// Constructs a ViewPageAdapter for the corresponding LearnActivity out of the vocbaularyItems of the list.
        /// </summary>
        /// <param name="activity">Corresponding LearnActivity which uses the Viewpager.</param>
        /// <param name="vocabularyItems">List of vocabularyItems of a category</param>
        public ViewPageAdapterLearn(LearnActivity activity, List<VocabularyItem> vocabularyItems)
        {
            this.activity = activity;
            this.vocabularyItems = vocabularyItems;
        }
        public override int Count => vocabularyItems.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == @object;
        }

        /// <summary>
        /// Instantiates the cooresponding view.
        /// </summary>
        /// <param name="container">The contaienr of the views</param>
        /// <param name="position">the position the viewpager is on.</param>
        /// <returns></returns>
        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            inflater = (LayoutInflater)activity.BaseContext.GetSystemService(Context.LayoutInflaterService);

            View vocabularyItemView = inflater.Inflate(Resource.Layout.B3_cat1, container, false);
            infoPressed = false;

            ImageView vocabularyImageView = vocabularyItemView.FindViewById<ImageView>(Resource.Id.vocabImg);
            string image = vocabularyItems[position].VocabularyImage;
            int resourceId = (int)typeof(Resource.Drawable).GetField(image).GetValue(null);
            vocabularyImageView.SetImageResource(resourceId);

            TextView vocabularyTextBavarian = vocabularyItemView.FindViewById<TextView>(Resource.Id.textBairisch);
            vocabularyTextBavarian.Text = vocabularyItems[position].VocabularyTextBavarian;

            TextView vocabularyTextGerman = vocabularyItemView.FindViewById<TextView>(Resource.Id.textHochdeutsch);
            vocabularyTextGerman.Text = vocabularyItems[position].VocabularyTextGerman;

            TextView vocabularyTextUserLanguage = vocabularyItemView.FindViewById<TextView>(Resource.Id.textArabisch);
            vocabularyTextUserLanguage.Text = vocabularyItems[position].VocabularyTextUserLanguage;


            showInfoButton(position, vocabularyItemView);

            playButton = vocabularyItemView.FindViewById<ImageButton>(Resource.Id.playSoundButton);
            playButton.Click += delegate
            {
                int soundId = (int)typeof(Resource.Raw).GetField(vocabularyItems[position].VocabularyAudio).GetValue(null);
                playButton = vocabularyItemView.FindViewById<ImageButton>(Resource.Id.playSoundButton);
                playButton.Enabled = false;
                playButton.SetImageResource(Resource.Drawable.playButtonInActive);
                player = MediaPlayer.Create(activity, soundId);
                player.Start();
                player.SetOnCompletionListener(this);
            };




            container.AddView(vocabularyItemView);

            return vocabularyItemView;
        }

        private void showInfoButton(int position, View vocabularyItemView)
        {
            ImageButton infoButton = vocabularyItemView.FindViewById<ImageButton>(Resource.Id.infoButton);
            if (vocabularyItems[position].VocabularyPopUpText == "")
            {
                infoButton.Visibility = Android.Views.ViewStates.Invisible;
                infoButton.Enabled = false;

            }
            else
            {
                infoButton.Click += delegate { GoToPopUp(vocabularyItemView, position); };
            }

        }

        /*private void PlaySound(int position, View vocabularyItemView)
        {

            int soundId = vocabularyItems[position].VocabularyAudioId;
            player = MediaPlayer.Create(activity, soundId);
            player.Looping = false;
            player.SetVolume(1.0f, 1.0f);
            player.Start();
            playButton = vocabularyItemView.FindViewById<ImageButton>(Resource.Id.playSoundButton);
            playButton.Enabled = false;
            playButton.SetImageResource(Resource.Drawable.playButtonInActive);
            player.SetOnCompletionListener(this);


        }*/

        private void GoToPopUp(View vocabularyItemView, int position)
        {
            TextView infoView = vocabularyItemView.FindViewById<TextView>(Resource.Id.popUpTextView);
            if (!infoPressed)
            {
                infoView.Text = vocabularyItems[position].VocabularyPopUpText;
                infoView.Visibility = ViewStates.Visible;
                infoPressed = true;
            }
            else
            {
                infoView.Visibility = ViewStates.Invisible;
                infoPressed = false;
            }
        }

        [Obsolete]
        public override void DestroyItem(View container, int position, Java.Lang.Object @object)
        {
            ((ViewGroup)container).RemoveView((View)@object);
        }

        public void OnCompletion(MediaPlayer mp)
        {
            mp.Release();
            playButton.Enabled = true;
            playButton.SetImageResource(Resource.Drawable.playButtonActive);
            activity.enableNavigation();
        }

    }
}