using SQLite;
using Syncfusion.XlsIO;
using System;
using System.Data;
using System.Reflection;
using Xamarin.Essentials;

namespace xamarin_app.Model
{
    public class DataBaseInitializator
    {
        //bei neuem Sheet auf Eigenschaften -> als Externe Resource einbinden
        private string welcomeToBavariaDocumentPath = "xamarin_app.ExcelDocuments.Welcome_to_Bavaria_Test.xlsx";
        private string categoryIconDocumentPath = "xamarin_app.ExcelDocuments.Category_Icon_Assignment.xlsx";

        private DataTable welcomeToBavariaDataTable;
        private DataTable categoryIconDataTable;


        private DataBaseManager manager = DataBaseManager.GetInstance();

        private static DataBaseInitializator INSTANCE;

        private DataBaseInitializator() { }

        public static DataBaseInitializator GetInstance()
        {
            if(INSTANCE == null)
            {
                INSTANCE = new DataBaseInitializator();
            }

            return INSTANCE;
        }
        public void InitializeDataBase()
        {
            //Initialize DB
            manager.CreateSQLiteConnection();
            SQLiteConnection conn = manager.GetSQLiteConnection();

            //manager.DropDataTables();

            //check, if the ecxel sheets are up to date
            checkForExcelUpdates();

            

            //create data tables, if not already done
            if (conn.GetTableInfo("Kategorie").Count == 0)
            {
                parseExcelSheets();
                manager.CreateKategorie(welcomeToBavariaDataTable);
            }

            if (conn.GetTableInfo("Bairisch").Count == 0)
            {
                manager.CreateBairisch(welcomeToBavariaDataTable);
            }

            if (conn.GetTableInfo("Aussprachen").Count == 0)
            {
                manager.CreateAussprachen(welcomeToBavariaDataTable);
            }

            if (conn.GetTableInfo("Bilder").Count == 0)
            {
                manager.CreateBilder(welcomeToBavariaDataTable);
            }

            if (conn.GetTableInfo("KategorieIcons").Count == 0)
            {
                manager.CreateKategorieIcons(categoryIconDataTable);
            }

            if (conn.GetTableInfo("Uebersetzungssprachen").Count == 0)
            {
                manager.CreateUebersetzungssprachen(welcomeToBavariaDataTable);
            }

            if (conn.GetTableInfo("Sprachen").Count == 0)
            {
                manager.CreateSprachen(welcomeToBavariaDataTable);
            }

            if (conn.GetTableInfo("SprachIcons").Count == 0)
            {
                manager.CreateSprachIcons(welcomeToBavariaDataTable);
            }
        }

        private void parseExcelSheets()
        {
            ExcelParser excelParser = new ExcelParser();

            //parse Welcome To Bavaria Document
            IWorkbook welocmeToBavariaWorkbook = excelParser.createWorkbook(welcomeToBavariaDocumentPath);
            welcomeToBavariaDataTable = excelParser.createDataTable(welocmeToBavariaWorkbook, 0);

            //parse Kategorinamen Document
            IWorkbook kategorienamenWorkbook = excelParser.createWorkbook(categoryIconDocumentPath);
            categoryIconDataTable = excelParser.createDataTable(kategorienamenWorkbook, 0);
        }

        private void checkForExcelUpdates()
        {
            string nameOfExcelOfPreferencesWelcome = "";
            string nameOfExcelOfPreferencesCategory = "";
            string nameOfSharedResourcesWelcome = GetSharedResources("Welcome");
            string nameOfSharedResourcesCategory = GetSharedResources("Category");

            //check Welcome To Bavaria Excel
            if (!Preferences.ContainsKey("WelcomeToBavariaExcel"))
            {
                Preferences.Set("WelcomeToBavariaExcel", "");
                //Console.WriteLine("Welcome initialized");
            }
            else
            {
                nameOfExcelOfPreferencesWelcome = Preferences.Get("WelcomeToBavariaExcel", "");
                //Console.WriteLine("Bavaria zugewiesen");
            }

            if (!nameOfExcelOfPreferencesWelcome.Equals(nameOfSharedResourcesWelcome))
            {
                Preferences.Set("WelcomeToBavariaExcel", nameOfSharedResourcesWelcome);
                manager.DropDataTables();
                //Console.WriteLine("Table Dropped");
            }

            //Check Category Icon Assignment Excel
            if (!Preferences.ContainsKey("CategoryIconExcel"))
            {
                Preferences.Set("CategoryIconExcel", "");
            }
            else
            {
                nameOfExcelOfPreferencesCategory = Preferences.Get("CategoryIconExcel", "");
                //Console.WriteLine("Category zugewiesen");
            }

            if (!nameOfExcelOfPreferencesCategory.Equals(nameOfSharedResourcesCategory))
            {
                Preferences.Set("CategoryIconExcel", nameOfSharedResourcesCategory);
                manager.DropDataTables();
            }

        }

        private string GetSharedResources(string name)
        {
            Assembly asmb = Assembly.GetExecutingAssembly();
            string[] resources = asmb.GetManifestResourceNames();

            string askedResourceName = "";

            foreach (string s in resources)
            {
                if (s.Contains(name))
                {
                    askedResourceName = s;
                }
            }

            return askedResourceName;
        }
    }
}
