
using Android.App;
using Android.Content.PM;

namespace xamarin_app.Droid
{
    [Activity(Label = "Welcome To Bavaria", Icon = "@mipmap/icon2", Theme = "@style/SplashTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}