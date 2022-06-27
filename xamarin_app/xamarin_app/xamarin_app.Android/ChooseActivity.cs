using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using xamarin_app.DataBaseClasses;
using xamarin_app.Logic;
using xamarin_app.Query;

/// <summary>
/// The Activity to choose a Category
/// </summary>
namespace xamarin_app.Droid
{
    [Activity(Label = "ChooseCategoryActivity", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class ChooseActivity : Activity
    {
        private List<Choose> categoriesItems;
        ListView listView;
        private string goalActivity;
        int goToId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.B3_choose_view);
            listView = FindViewById<ListView>(Resource.Id.categoriesView);

            goalActivity = Intent.GetStringExtra("from");


            TextView textView = FindViewById<TextView>(Resource.Id.categoriesText);
            if (goalActivity == "Learn")
            {
                categoriesItems = Categories.GetCategories(true);
                textView.Text = "Wähle eine Kategorie zum Lernen";
            }
            else if (goalActivity == "Test")
            {
                categoriesItems = Categories.GetCategories(false);
                textView.Text = "Wähle eine Kategorie zum Abfragen";
            }
            else if (goalActivity == "Language")
            {
                categoriesItems = SettingsQuerys.GetLanguages();
                textView.Text = "Wähle deine Sprache aus";
            }

            listView.Adapter = new ChooseAdapter(this, categoriesItems);

            listView.ItemClick += ListViewItemClick;
        }

        /// <summary>
        /// Handles the action of a click on a listview Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            goToId = categoriesItems[e.Position].GetId();

            //TODO für Release auskommentieren
            //gibt alle falschen Items aus
            string wrongItems = "";
            foreach (Bairisch item in Querys.GetItems(goToId))
            {
                if (
                    String.IsNullOrEmpty(item.vokabel) ||
                    String.IsNullOrEmpty(Querys.GetTranslation(1, item.bairischId)) ||
                    String.IsNullOrEmpty(Querys.GetTranslation(Settings.Language, item.bairischId))
                    )
                {
                    wrongItems += "Vocab " + item.bairischId + ": " + item.vokabel + " has invalid name or translation.\n";
                }
                else
                {
                    try
                    {
                        string tmp = Querys.GetPicture(item.bildId);
                        int test = 0;
                        test = (int)typeof(Resource.Drawable).GetField(tmp).GetValue(null);
                    }
                    catch
                    {
                        wrongItems += "Vocab " + item.bairischId + ": " + item.vokabel + " has an invalid picture.\n";
                    }
                    try
                    {
                        int test = (int)typeof(Resource.Raw).GetField(Querys.GetAudio(item.ausspracheId)).GetValue(null);
                    }
                    catch
                    {
                        wrongItems += "Vocab " + item.bairischId + ": " + item.vokabel + " has an invalid audio.\n";
                    }
                }
            }
            if (!wrongItems.Equals(""))
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("There are invalid items in this category");
                builder.SetMessage(wrongItems);
                builder.SetCancelable(false);
                builder.SetPositiveButton("OK", YesAction);
                builder.Create().Show();
            }
            else
                //--------------------------------------------------------------------------------------
                GoToCategory(goToId);
        }

        private void YesAction(object sender, DialogClickEventArgs e)
        {
            GoToCategory(goToId);
        }

        protected void GoToCategory(int ID)
        {
            Finish();
            Intent intent = new Intent();
            if (goalActivity == "Learn")
            {
                intent = new Intent(this, typeof(LearnActivity));
                intent.PutExtra("key", ID);
                StartActivity(intent);
            }
            else if (goalActivity == "Test")
            {
                intent = new Intent(this, typeof(TestActivity));
                intent.PutExtra("key", ID);
                StartActivity(intent);
            }
            else if (goalActivity == "Language")
            {
                SettingsQuerys.SetLanguage(ID);
            }
        }
    }
}