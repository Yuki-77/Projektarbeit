using Xamarin.Forms;
using xamarin_app.Model;

namespace xamarin_app
{
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            //muss vor InitializeComponents() stehen!!
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTM2MTg2QDMxMzcyZTMyMmUzME1NMitCZGNEZld6VkxTZTVNNWpod1UxU2dOMW1TNEpBV0ZVZXpzdGlsZG89");

            InitializeComponent();

            MainPage = new WelcomePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            DataBaseInitializator initializator = DataBaseInitializator.GetInstance();
            initializator.InitializeDataBase();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
