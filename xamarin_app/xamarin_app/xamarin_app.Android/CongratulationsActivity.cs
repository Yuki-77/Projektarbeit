using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using NL.DionSegijn.Konfetti;
using NL.DionSegijn.Konfetti.Models;
using System;
using xamarin_app.Logic;
using static Android.Media.MediaPlayer;

/// <summary>
/// Activity Shown at the End of an LearnActivity
/// </summary>
namespace xamarin_app.Droid
{
    [Activity(Label = "CongratulationsActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class CongratulationsActivity : AppCompatActivity, IOnCompletionListener
    {
        private string screenResultsFrom;
        private int categoryId;
        private KonfettiView konfettiView;
        private int correctItems;
        private int totalItems;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B4_congrats);
            konfettiView = FindViewById<KonfettiView>(Resource.Id.konfettiView2);
            categoryId = Intent.GetIntExtra("categoryID", -1);
            screenResultsFrom = Intent.GetStringExtra("from");

            if (screenResultsFrom.Equals("AbfragenActivity"))
            {
                TextView upper = FindViewById<TextView>(Resource.Id.textView1);
                TextView lower = FindViewById<TextView>(Resource.Id.textView2);
                correctItems = Intent.GetIntExtra("correctItems", -1);
                totalItems = Intent.GetIntExtra("totalItems", -1);
                int percent = (100 / totalItems) * correctItems;
                MediaPlayer player;
                if (percent == 100)
                {
                    upper.Text = "Sauber sog i!";
                    lower.Text = "Sehr gut gemacht!";
                    int sound = Resource.Raw.sauber_sog_i_m;
                    player = MediaPlayer.Create(this, sound);
                }
                else if (percent >= 90)
                {
                    upper.Text = "Guad gmacht!";
                    lower.Text = "Gut gemacht!";
                    int sound = Resource.Raw.guad_gmacht_m;
                    player = MediaPlayer.Create(this, sound);
                }
                else if (percent >= 70)
                {
                    upper.Text = "Kannd noch was werdn!";
                    lower.Text = "Kann noch etwas werden!";
                    int sound = Resource.Raw.kannd_noch_was_werdn_m;
                    player = MediaPlayer.Create(this, sound);
                }
                else if (percent >= 50)
                {
                    upper.Text = "Ned schlecht!";
                    lower.Text = "Nicht schlecht";
                    int sound = Resource.Raw.ned_schlecht_m;
                    player = MediaPlayer.Create(this, sound);
                }
                else if (percent >= 20)
                {
                    upper.Text = "Probiers nochemol!";
                    lower.Text = "Probier es noch einmal!";
                    int sound = Resource.Raw.probiers_nochemol_m;
                    player = MediaPlayer.Create(this, sound);
                }
                else if (percent >= 1)
                {
                    upper.Text = "Au e zwick!";
                    lower.Text = "Auweia";
                    int sound = Resource.Raw.au_e_zwick_m;
                    player = MediaPlayer.Create(this, sound);
                }
                else
                {
                    upper.Text = "Schod fir d Zeit.";
                    lower.Text = "Schade für die Zeit.";
                    int sound = Resource.Raw.schod_fir_d_zeit_m;
                    player = MediaPlayer.Create(this, sound);
                }
                player.Start();
                player.SetOnCompletionListener(this);
                FindViewById(Resource.Id.correctOnesLayout).Visibility = ViewStates.Visible;
                FindViewById<TextView>(Resource.Id.correctItemsText).Text = correctItems.ToString();
                FindViewById<TextView>(Resource.Id.maxItemsText).Text = totalItems.ToString();
                FindViewById<TextView>(Resource.Id.abfrageText).Text = "Erneut Abfragen";


            }
            else if (screenResultsFrom.Equals("LearnActivity"))
            {
                FindViewById(Resource.Id.correctOnesLayout).Visibility = ViewStates.Gone;
                PlaySound();
            }
            Konfetti();
            FindViewById(Resource.Id.goToAbfrageButton).Click += GoToAbfragen;
            FindViewById(Resource.Id.goToMainButton).Click += delegate { GoToMain(); };
        }

        private void Konfetti()
        {
            konfettiView.Build()
                .SetPosition(-50f, 1000f, -50f, -50f)
                .SetDirection(0.0, 396.0)
                .AddColors(Color.LightBlue, Color.LightSkyBlue, Color.WhiteSmoke, Color.GhostWhite)
                .SetSpeed(4f, 7f)
              .SetFadeOutEnabled(true)
                     .SetTimeToLive(2000L)
                     .AddShapes(Shape.Rect, Shape.Circle)
                     .AddSizes(new Size(12, 12), new Size(16, 6f))
                     .StreamFor(300, 3000L);
        }

        //use soundpool to play audio
        protected void PlaySound()
        {
            if (SettingsQuerys.IsSoundOn())
            {
                int soundId = Resource.Raw.Congrats;
                MediaPlayer player = MediaPlayer.Create(this, soundId);
                try
                {
                    player.Looping = false;
                    player.SetVolume(1.0f, 1.0f);
                    player.Start();
                }
                finally
                {
                    player.Dispose();
                }
            }
        }

        /*protected void runGif()
        {
            AnimationDrawable _asteroidDrawable;
            _asteroidDrawable = (Android.Graphics.Drawables.AnimationDrawable)
            Resources.GetDrawable(Resource.Drawable.konfettiImages);
            ImageView asteroidImage = FindViewById<ImageView>(Resource.Id.konfettiGif);
            asteroidImage.SetImageDrawable((Android.Graphics.Drawables.Drawable)_asteroidDrawable);
            _asteroidDrawable.Start();


        }*/
        private void GoToMain()
        {
            Finish();
        }

        private void GoToAbfragen(object sender, EventArgs e)
        {
            Finish();
            Intent intent = new Intent(this, typeof(TestActivity));
            intent.PutExtra("key", categoryId);
            StartActivity(intent);
        }

        public void OnCompletion(MediaPlayer mp)
        {
            mp.Release();
        }

        public override void OnBackPressed()
        {
            GoToMain();

        }
    }
}