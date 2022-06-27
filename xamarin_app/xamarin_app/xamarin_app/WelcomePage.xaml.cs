using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xamarin_app
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        //Calls after a delay of 1sec the MainMenu
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);
            App.Current.MainPage = new MainMenuPage();
        }
    }
}
