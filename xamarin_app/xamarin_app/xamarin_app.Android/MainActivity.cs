using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using xamarin_app.Logic;
using static Android.Media.MediaPlayer;

/// <summary>
/// Is the main activiy containing the main menu
/// </summary>
namespace xamarin_app.Droid
{
    [Activity(Label = "Welcome To Bavaria", Icon = "@mipmap/icon2", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity, IOnCompletionListener
    {
        public static MainActivity Instance;
        private MediaPlayer player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

            if (SettingsQuerys.IsSoundOn())
            {
                int sound = Resource.Raw.welcome_to_bavaria_m;
                player = MediaPlayer.Create(this, sound);
                player.Start();
                player.SetOnCompletionListener(this);
            }

            SetContentView(Resource.Layout.B3_mainMenu);

            if (!SettingsQuerys.IsLanguageSet())
                GoToLanguage();

            FindViewById(Resource.Id.lernenButton).Click += GoToLearn;
            FindViewById(Resource.Id.abfragenButton).Click += GoToTest;
            FindViewById(Resource.Id.settingsButton).Click += GoToSettings;
        }

        /// <summary>
        /// navigation to the Lern-Activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToLearn(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ChooseActivity));
            intent.PutExtra("from", "Learn");
            StartActivity(intent);
        }

        /// <summary>
        /// navigation to the Abfrage-Activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToTest(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ChooseActivity));
            intent.PutExtra("from", "Test");
            StartActivity(intent);
        }

        /// <summary>
        /// navigation to the settings-page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToSettings(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingsActivity));
            intent.PutExtra("from", "Settting");
            StartActivity(intent);
        }

        /// <summary>
        /// navigation to the Abfrage-Activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToLanguage()
        {
            Intent intent = new Intent(this, typeof(ChooseActivity));
            intent.PutExtra("from", "Language");
            StartActivity(intent);
        }

        public void OnCompletion(MediaPlayer mp)
        {
            mp.Release();
        }

        public override void OnBackPressed()
        {
            this.MoveTaskToBack(true);
        }
    }
}