
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;
using xamarin_app.Logic;

/// <summary>
/// The Activity for Settings
/// </summary>
namespace xamarin_app.Droid
{
    [Activity(Label = "EinstellungActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B2_Settings);

            FindViewById(Resource.Id.spracheButton).Click += GoToLanguage;
            FindViewById(Resource.Id.aboutusButton).Click += GoToAboutUs;
            FindViewById(Resource.Id.resetButton).Click += OpenResetPopUp;
            FindViewById(Resource.Id.referenceButton).Click += GoToReferences;

            Switch soundSwitch = FindViewById<Switch>(Resource.Id.soundSwitch);
            soundSwitch.SetSwitchTextAppearance(this, Resource.Style.Switch);
            soundSwitch.Checked = SettingsQuerys.IsSoundOn();
            soundSwitch.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
            {
                if (e.IsChecked)
                    SettingsQuerys.SetSound(true);
                else
                    SettingsQuerys.SetSound(false);
            };
        }



        private void OpenResetPopUp(object sender, EventArgs e)
        {
            AlertDialog.Builder builder
= new AlertDialog
      .Builder(this);

            builder.SetTitle("Wirklich den Fortschritt zurücksetzen?");
            builder.SetMessage("Bei Bestätigung werden alle Fortschritte des Lernens und Abfragens zurückgesetzt.");
            builder.SetCancelable(false);
            builder.SetPositiveButton("Zurücksetzen", ResetAction);
            builder.SetNegativeButton("Abbrechen", CancelAction);
            builder.Create().Show();
        }


        private void ResetAction(object sender, DialogClickEventArgs e)
        {

            SettingsQuerys.Reset();
        }
        private void CancelAction(object sender, DialogClickEventArgs e)
        {

        }

        /// <summary>
        /// navigation to the Abfrage-Activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToLanguage(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ChooseActivity));
            intent.PutExtra("from", "Language");
            StartActivity(intent);
        }
        protected void GoToAboutUs(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(AboutUsActivity)));
        }
        private void GoToReferences(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(ReferenceActivity)));
        }




    }
}