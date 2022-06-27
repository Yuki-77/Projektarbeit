using Android.Content;
using Android.Media;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using xamarin_app.Logic;
using static Android.Media.MediaPlayer;

namespace xamarin_app.Droid
{
    class ViewPageAdapterTest : PagerAdapter, IOnCompletionListener
    {
        private readonly TestActivity activity;
        private MediaPlayer player;
        private LayoutInflater inflater;
        private ImageButton playButton;
        private readonly List<TestVocabs> vocabularyItems;
        private bool clickable;
        private bool firstClick;

        public ViewPageAdapterTest(TestActivity abfragenActivity, List<TestVocabs> vocabularyItems)
        {
            activity = abfragenActivity;
            this.vocabularyItems = vocabularyItems;
        }

        public override int Count => vocabularyItems.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == @object;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            inflater = (LayoutInflater)activity.BaseContext.GetSystemService(Context.LayoutInflaterService);

            View vocabularyItemView = inflater.Inflate(Resource.Layout.B4_Test, container, false);

            List<TestItem> tmp = vocabularyItems[position].GetPictures();

            clickable = false;
            firstClick = true;

            ImageView vocabularyImageView1 = vocabularyItemView.FindViewById<ImageView>(Resource.Id.vocabImg1);
            string image1 = tmp[0].GetPicture();
            int resourceId1 = (int)typeof(Resource.Drawable).GetField(image1).GetValue(null);
            vocabularyImageView1.SetImageResource(resourceId1);

            TextView vocabularyText1 = vocabularyItemView.FindViewById<TextView>(Resource.Id.vocabText1);
            vocabularyText1.Text = tmp[0].GetVocab();

            ImageView vocabularyImageView2 = vocabularyItemView.FindViewById<ImageView>(Resource.Id.vocabImg2);
            string image2 = tmp[1].GetPicture();
            int resourceId2 = (int)typeof(Resource.Drawable).GetField(image2).GetValue(null);
            vocabularyImageView2.SetImageResource(resourceId2);

            TextView vocabularyText2 = vocabularyItemView.FindViewById<TextView>(Resource.Id.vocabText2);
            vocabularyText2.Text = tmp[1].GetVocab();

            ImageView vocabularyImageView3 = vocabularyItemView.FindViewById<ImageView>(Resource.Id.vocabImg3);
            string image3 = tmp[2].GetPicture();
            int resourceId3 = (int)typeof(Resource.Drawable).GetField(image3).GetValue(null);
            vocabularyImageView3.SetImageResource(resourceId3);

            TextView vocabularyText3 = vocabularyItemView.FindViewById<TextView>(Resource.Id.vocabText3);
            vocabularyText3.Text = tmp[2].GetVocab();

            ImageView vocabularyImageView4 = vocabularyItemView.FindViewById<ImageView>(Resource.Id.vocabImg4);
            string image4 = tmp[3].GetPicture();
            int resourceId4 = (int)typeof(Resource.Drawable).GetField(image4).GetValue(null);
            vocabularyImageView4.SetImageResource(resourceId4);

            TextView vocabularyText4 = vocabularyItemView.FindViewById<TextView>(Resource.Id.vocabText4);
            vocabularyText4.Text = tmp[3].GetVocab();

            vocabularyImageView1.Click += delegate
            {
                if (clickable && vocabularyItems[position].GetPictures()[0].GetIconMatchesSound())
                {
                    RightItem(vocabularyImageView1);
                }
                else if (clickable)
                {
                    WrongItem(position, vocabularyImageView1, vocabularyImageView1, vocabularyImageView2, vocabularyImageView3, vocabularyImageView4);
                }
            };
            vocabularyImageView2.Click += delegate
            {
                if (clickable && vocabularyItems[position].GetPictures()[1].GetIconMatchesSound())
                {
                    RightItem(vocabularyImageView2);
                }
                else if (clickable)
                {
                    WrongItem(position, vocabularyImageView2, vocabularyImageView1, vocabularyImageView2, vocabularyImageView3, vocabularyImageView4);
                }
            };
            vocabularyImageView3.Click += delegate
            {
                if (clickable && vocabularyItems[position].GetPictures()[2].GetIconMatchesSound())
                {
                    RightItem(vocabularyImageView3);
                }
                else if (clickable)
                {
                    WrongItem(position, vocabularyImageView3, vocabularyImageView1, vocabularyImageView2, vocabularyImageView3, vocabularyImageView4);
                }
            };
            vocabularyImageView4.Click += delegate
            {
                if (clickable && vocabularyItems[position].GetPictures()[3].GetIconMatchesSound())
                {
                    RightItem(vocabularyImageView4);
                }
                else if (clickable)
                {
                    WrongItem(position, vocabularyImageView4, vocabularyImageView1, vocabularyImageView2, vocabularyImageView3, vocabularyImageView4);
                }
            };

            playButton = vocabularyItemView.FindViewById<ImageButton>(Resource.Id.playSoundButton);
            playButton.Click += delegate
            {
                int soundId = (int)typeof(Resource.Raw).GetField(vocabularyItems[position].GetAudio()).GetValue(null);
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

        private void RightItem(View view)
        {
            (view).SetBackgroundResource(Resource.Layout.GreenBorder);

            if (firstClick)
            {
                if (SettingsQuerys.IsSoundOn())
                {
                    int soundId = Resource.Raw.training_program_correct1;
                    player = MediaPlayer.Create(activity, soundId);
                    player.Start();
                }
                TestActivity.rightItems++;
            }
            TestActivity.nextButton.Visibility = ViewStates.Visible;
            try
            {
                TestActivity.nextButton.StartAnimation(TestActivity.anim);
            }
            catch
            {
                //...
            }
            firstClick = false;
            clickable = false;
            TestActivity.vocabularyPager.SetPagingEnabled(true);
        }

        private void WrongItem(int position, View view, View otherView1, View otherView2, View otherView3, View otherView4)
        {
            firstClick = false;
            (view).SetBackgroundResource(Resource.Layout.RedBorder);

            if (SettingsQuerys.IsSoundOn())
            {
                int soundId = Resource.Raw.training_program_incorrect1;
                player = MediaPlayer.Create(activity, soundId);
                player.Start();
            }
            if (vocabularyItems[position].GetPictures()[0].GetIconMatchesSound())
            {
                otherView1.PerformClick();
            }
            else if (vocabularyItems[position].GetPictures()[1].GetIconMatchesSound())
            {
                otherView2.PerformClick();
            }
            else if (vocabularyItems[position].GetPictures()[2].GetIconMatchesSound())
            {
                otherView3.PerformClick();
            }
            else if (vocabularyItems[position].GetPictures()[3].GetIconMatchesSound())
            {
                otherView4.PerformClick();
            }
        }

        [Obsolete]
        public override void DestroyItem(View container, int position, Java.Lang.Object @object)
        {
            player.Release();
            ((ViewGroup)container).RemoveView((View)@object);
        }

        public void OnCompletion(MediaPlayer mp)
        {
            mp.Release();
            playButton.Enabled = true;
            playButton.SetImageResource(Resource.Drawable.playButtonActive);
            if (firstClick)
            {
                clickable = true;
            }
        }
    }
}