
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace xamarin_app.Droid
{
    [Activity(Label = "AboutUsActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class AboutUsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B2_Settings_AboutUs);
            TextView welcomeTextView = FindViewById<TextView>(Resource.Id.welcomeToBavariaTextView);
            TextView noteTextView = FindViewById<TextView>(Resource.Id.noteTextView);
            TextView thanksTextView = FindViewById<TextView>(Resource.Id.thanksTextView);

            //TextView textView = FindViewById<TextView>(Resource.Id.categoriesText2);
            //textView.Text = "Wähle eine Kategorie zum Lernen";

            if (Settings.Language == 2) //Arabisch
            {
                welcomeTextView.TextDirection = TextDirection.AnyRtl;
                noteTextView.TextDirection = TextDirection.AnyRtl;
                thanksTextView.TextDirection = TextDirection.AnyRtl;
                welcomeTextView.Text = Documents.AboutUs.WelcomeArabic;
                noteTextView.Text = Documents.AboutUs.NoteArabic;
                thanksTextView.Text = Documents.AboutUs.ThanksArabic;
            }
            else if (Settings.Language == 3) //Englisch
            {
                welcomeTextView.Text = Documents.AboutUs.WelcomeEnglish;
                noteTextView.Text = Documents.AboutUs.NoteEnglish;
                thanksTextView.Text = Documents.AboutUs.ThanksEnglish;
            }
            else if (Settings.Language == 4) //Tschechisch
            {
                welcomeTextView.Text = Documents.AboutUs.WelcomeCzech;
                noteTextView.Text = Documents.AboutUs.NoteCzech;
                thanksTextView.Text = Documents.AboutUs.ThanksCzech;
            }
            else //Hochdeutsch
            {
                welcomeTextView.Text = Documents.AboutUs.WelcomeGerman;
                noteTextView.Text = Documents.AboutUs.NoteGerman;
                thanksTextView.Text = Documents.AboutUs.ThanksGerman;
            }
        }
    }
}