using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xamarin.Forms;
using xamarin_app.DataBaseClasses;
using xamarin_app.Helpers;

namespace xamarin_app.Model
{
    class DataBaseManager
    {
        private static DataBaseManager INSTANCE = null;
        private static SQLiteConnection sqliteConnection = null;
        private string dataBaseName = "SprachlernappBairisch.db";

        private DataBaseManager()
        {

        }

        public void CreateSQLiteConnection()
        {
            sqliteConnection = DependencyService.Get<ISQLite>().GetConnection(dataBaseName);
        }

        public void DropDataTables()
        {
            sqliteConnection.DropTable<Bairisch>();
            sqliteConnection.DropTable<Kategorie>();
            sqliteConnection.DropTable<Aussprachen>();
            sqliteConnection.DropTable<Bilder>();
            sqliteConnection.DropTable<KategorieIcons>();
            sqliteConnection.DropTable<Uebersetzungssprachen>();
            sqliteConnection.DropTable<Sprachen>();
            sqliteConnection.DropTable<SprachIcons>();
        }


        public void CreateBairisch(DataTable dataTable)
        {
            sqliteConnection.CreateTable<Bairisch>();
            //for the PK and FK's, they start with 1
            int i = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                string katName = row["Kapitel"].ToString();
                IEnumerable<Kategorie> katId = sqliteConnection.Query<Kategorie>("select * from Kategorie where name = ?", katName);


                sqliteConnection.Insert(new Bairisch
                {
                    bairischId = i,
                    kategorieId = katId.ElementAt(0).kategorieId,
                    vokabel = row["Bairisch"].ToString(),
                    woertlich = row["wörtlich"].ToString(),
                    bemerkung = row["Bemerkung"].ToString(),
                    bildId = i,
                    ausspracheId = i
                });

                //Console.WriteLine("Vokabel: " + row["Bairisch"].ToString());
                //Console.WriteLine("wörtlich: " + row["wörtlich"].ToString());
                //Console.WriteLine("Bemerkung: " + row["Bemerkung"].ToString());

                i++;
            }
        }


        public void CreateKategorie(DataTable dataTable)
        {
            sqliteConnection.CreateTable<Kategorie>();

            //for the PK and FK's, they start with 1
            int i = 1;

            string predecessor = "";
            string actual = "";

            foreach (DataRow row in dataTable.Rows)
            {
                actual = row["Kapitel"].ToString();

                if (!actual.Equals(predecessor))
                {
                    sqliteConnection.Insert(new Kategorie
                    {
                        kategorieId = i,
                        name = actual,
                        kategorieIconId = i
                    });

                    i++;
                    predecessor = actual;
                }


            }
        }

        public void CreateAussprachen(DataTable dataTable)
        {
            sqliteConnection.CreateTable<Aussprachen>();

            //for the PK and FK's, they start with 1
            int i = 1;

            foreach (DataRow row in dataTable.Rows)
            {
                sqliteConnection.Insert(new Aussprachen
                {
                    ausspracheId = i,
                    aussprache = row["Resourcename"].ToString() + "_m" //"_m" für männliche Sprachaufnahmen
                                                                       //wenn weibliche dazukommt, evtl. ändern/Auswahl erstellen

                });

                i++;
            }
        }

        public void CreateBilder(DataTable dataTable)
        {
            sqliteConnection.CreateTable<Bilder>();

            //for the PK and FK's, they start with 1
            int i = 1;

            foreach (DataRow row in dataTable.Rows)
            {

                sqliteConnection.Insert(new Bilder
                {
                    bildId = i,
                    bild = row["Resourcename"].ToString()
                });

                //Console.WriteLine("Bild: " + row["Resourcename"].ToString());

                i++;
            }
        }


        public void CreateKategorieIcons(DataTable dataTable)
        {
            sqliteConnection.CreateTable<KategorieIcons>();

            foreach (DataRow row in dataTable.Rows)
            {
                sqliteConnection.Insert(new KategorieIcons
                {
                    kategorieIconId = Convert.ToInt32(row["ID"].ToString()),
                    kategorieIcon = row["Resourcepfad"].ToString()
                }); ;
            }
        }

        public void CreateUebersetzungssprachen(DataTable dataTable)
        {

            sqliteConnection.CreateTable<Uebersetzungssprachen>();

            //for the PK and FK's, they start with 1
            int uebersetzungsSpracheIdCounter = 1;
            int bairischIdCounter = 1;

            foreach (DataRow row in dataTable.Rows)
            {
                sqliteConnection.Insert(new Uebersetzungssprachen
                {
                    uebersetzungsspracheId = uebersetzungsSpracheIdCounter,
                    spracheId = 1,
                    vokabel = row["Hochdeutsch/Standardsprache"].ToString(),
                    bairischId = bairischIdCounter
                });

                uebersetzungsSpracheIdCounter++;
                bairischIdCounter++;
            }

            //auf 1 zurücksetzten, da neue Sprache anfängt
            bairischIdCounter = 1;

            foreach (DataRow row in dataTable.Rows)
            {
                sqliteConnection.Insert(new Uebersetzungssprachen
                {
                    uebersetzungsspracheId = uebersetzungsSpracheIdCounter,
                    spracheId = 2,
                    vokabel = row["Arabisch"].ToString(),
                    bairischId = bairischIdCounter
                });

                uebersetzungsSpracheIdCounter++;
                bairischIdCounter++;
            }

            //auf 1 zurücksetzten, da neue Sprache anfängt
            bairischIdCounter = 1;

            foreach (DataRow row in dataTable.Rows)
            {
                sqliteConnection.Insert(new Uebersetzungssprachen
                {
                    uebersetzungsspracheId = uebersetzungsSpracheIdCounter,
                    spracheId = 3,
                    vokabel = row["Englisch"].ToString(),
                    bairischId = bairischIdCounter
                });

                uebersetzungsSpracheIdCounter++;
                bairischIdCounter++;
            }

            //auf 1 zurücksetzten, da neue Sprache anfängt
            bairischIdCounter = 1;

            foreach (DataRow row in dataTable.Rows)
            {
                sqliteConnection.Insert(new Uebersetzungssprachen
                {
                    uebersetzungsspracheId = uebersetzungsSpracheIdCounter,
                    spracheId = 4,
                    vokabel = row["Tschechisch"].ToString(),
                    bairischId = bairischIdCounter
                });

                uebersetzungsSpracheIdCounter++;
                bairischIdCounter++;
            }
        }

        public void CreateSprachen(DataTable dataTable)
        {
            sqliteConnection.CreateTable<Sprachen>();

            sqliteConnection.Insert(new Sprachen
            {
                spracheId = 1,
                sprache = "Hochdeutsch",
                sprachIconId = 1
            });

            sqliteConnection.Insert(new Sprachen
            {
                spracheId = 2,
                sprache = "Arabisch",
                sprachIconId = 2
            });

            sqliteConnection.Insert(new Sprachen
            {
                spracheId = 3,
                sprache = "Englisch",
                sprachIconId = 3
            });

            sqliteConnection.Insert(new Sprachen
            {
                spracheId = 4,
                sprache = "Tschechisch",
                sprachIconId = 4
            });

        }

        //TODO
        public void CreateSprachIcons(DataTable dataTable)
        {
            sqliteConnection.CreateTable<SprachIcons>();

            sqliteConnection.Insert(new SprachIcons
            {
                sprachIconId = 1,
                sprachIcon = "flag_ger"
            });

            sqliteConnection.Insert(new SprachIcons
            {
                sprachIconId = 2,
                sprachIcon = "flag_arableague"
            });

            sqliteConnection.Insert(new SprachIcons
            {
                sprachIconId = 3,
                sprachIcon = "flag_usa"
            });

            sqliteConnection.Insert(new SprachIcons
            {
                sprachIconId = 4,
                sprachIcon = "flag_cz"
            });
        }

        public static DataBaseManager GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new DataBaseManager();
            }

            return INSTANCE;
        }

        public SQLiteConnection GetSQLiteConnection()
        {
            return sqliteConnection;
        }

    }

}
