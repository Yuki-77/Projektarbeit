using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Text.Method;
using Android.Text.Util;
using Android.Widget;
using System;

namespace xamarin_app.Droid
{
    [Activity(Label = "ReferenceActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class ReferenceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B2_Settings_References);
            TextView iconReferencesText = FindViewById<TextView>(Resource.Id.iconReferencesText);
            TextView imageReferencesText = FindViewById<TextView>(Resource.Id.imageReferencesText);
            TextView effectReferencesText = FindViewById<TextView>(Resource.Id.effectReferencesText);
            TextView soundReferencesText = FindViewById<TextView>(Resource.Id.soundReferencesText);

            String iconText = "Icons: \n" +
                "www.icons8.com";
            iconReferencesText.Text = iconText;
            iconReferencesText.SetLinkTextColor(Color.ParseColor("#0000ff"));
            Linkify.AddLinks(iconReferencesText, MatchOptions.WebUrls);
            iconReferencesText.MovementMethod = LinkMovementMethod.Instance;

            String imagesText = "Bilder für Vokabeln: \n" +
                "www.pixabay.com \n" +
                "www.unsplash.com \n" +
                "www.pexels.com \n" +
                "www.pxhere.com \n" +
                "https://commons.wikimedia.org \n" +
                "\n" +
                "Bilder für Kategorien: \n" +
                "www.unsplash.com \n" +
                "www.pixabay.com \n" +
                "\n" +
                "Bilder für Flaggen: \n" +
                "www.countryflags.com \n" +
                "https://de.wikipedia.org/wiki/Datei:Flag_of_the_Arab_League.svg#filelinks \n" +
                "\n" +
                "Alle Bilder sind unter der Creative Commons CC0 oder leicht angepassten Lizenzen verwendbar und dürfen sowohl unkommerziell, als auch kommerziell verwendet werden.";

            imageReferencesText.Text = imagesText;
            imageReferencesText.SetLinkTextColor(Color.ParseColor("#0000ff"));
            Linkify.AddLinks(imageReferencesText, MatchOptions.WebUrls);
            imageReferencesText.MovementMethod = LinkMovementMethod.Instance;


            String effectText = "Konfetti-Effekt: \n" +
                "https://github.com/DanielMartinus/Konfetti \n" +
                "\n" +
                "Nutzbar unter der ISC-License: Copyright(c) 2017 Dion Segijn.";

            effectReferencesText.Text = effectText;
            effectReferencesText.SetLinkTextColor(Color.ParseColor("#0000ff"));
            Linkify.AddLinks(effectReferencesText, MatchOptions.WebUrls);
            effectReferencesText.MovementMethod = LinkMovementMethod.Instance;

            String soundText = "Hinweistöne: \n" +
                "http://tale3habet.eb2a.com/ \n" +
                "http://www.aigei.com/ \n" +
                "\n" +
                "Alle Sounds sind unter der Creative Commons CC0 Lizenz verwendbar.";
            soundReferencesText.Text = soundText;
            soundReferencesText.SetLinkTextColor(Color.ParseColor("#0000ff"));
            Linkify.AddLinks(soundReferencesText, MatchOptions.WebUrls);
            soundReferencesText.MovementMethod = LinkMovementMethod.Instance;

            /*String val1 = "Icons von Icons8";
            iconReferencesText.Text = val1;

            Java.Util.Regex.Pattern pattern2 = Java.Util.Regex.Pattern.Compile("Icons8");

            Java.Util.Regex.Pattern pattern = Java.Util.Regex.Pattern.Compile("^(.*?)(\bIcons8\b)(.*)$");
            String scheme = "https://www.icons8.com/";
            Linkify.AddLinks(iconReferencesText, pattern2, scheme);
            iconReferencesText.MovementMethod = LinkMovementMethod.Instance; */

            /*String html = "<a href=\"http://www.google.com\">Google</a>";
            Spanned result = HtmlCompat.fromHtml(html, Html.FROM_HTML_MODE_LEGACY);
            txtTest.setText(result);
            txtTest.setMovementMethod(LinkMovementMethod.getInstance());*/
            // Create your application here
        }
    }
}