using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        //protected async override void OnAppearing()
        //{
        //    NavigationPage.SetHasNavigationBar(this, false);
        //}

        Button buttonLernen = new Button
        {
            Text = "button text",
            Image = new FileImageSource
            {
                File = "image filename"
            },
            ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 20)
        };

        Button buttonAbfragen = new Button
        {
            Text = "button text",
            Image = new FileImageSource
            {
                File = "image filename"
            },
            ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 20)
        };

        async void GoToLernen(object sender, EventArgs args)
        {
            await DisplayAlert("Sorry", "This function is not implemented yet", "Exit");
        }
        async void GotoAbfragen(object sender, EventArgs args)
        {
            await DisplayAlert("Sorry", "This function is not implemented yet", "Exit");
        }
    }
}