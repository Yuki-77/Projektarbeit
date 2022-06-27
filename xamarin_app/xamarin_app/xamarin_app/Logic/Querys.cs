using SQLite;
using System.Collections.Generic;
using xamarin_app.DataBaseClasses;
using xamarin_app.Model;

namespace xamarin_app
{
    public static class Querys
    {

        private static readonly DataBaseManager manager = DataBaseManager.GetInstance();

        /// <summary>
        /// This method returns all Categories as List
        /// </summary>
        public static List<Kategorie> GetCategories()
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "SELECT * FROM Kategorie ORDER BY kategorieId"
            };
            List<Kategorie> cat = command.ExecuteQuery<Kategorie>();
            return cat;
        }

        /// <summary>
        /// This method returns all Sprachen as List
        /// </summary>
        public static List<Sprachen> GetLanguages()
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "SELECT * FROM Sprachen ORDER BY spracheId"
            };
            List<Sprachen> lang = command.ExecuteQuery<Sprachen>();
            return lang;
        }

        /// <summary>
        /// This method returns all items of given Kapitel as List of Bairisch
        /// </summary>
        /// <param name="id">The ID of the required Kapitel</param>
        public static List<Bairisch> GetItems(int id)
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "select * from Bairisch where kategorieId = " + id
            };
            List<Bairisch> items = command.ExecuteQuery<Bairisch>();
            return items;
        }

        /// <summary>
        /// This method returns the path of the Audiofile of given id as string
        /// </summary>
        /// <param name="id">The ID of the required Audio</param>
        public static string GetAudio(int id)
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "select aussprache from Aussprachen where ausspracheId = " + id + " LIMIT 1"
            };
            ;
            string audio = command.ExecuteScalar<string>();
            return audio;
        }

        /// <summary>
        /// This method returns the name of the Picture as string
        /// </summary>
        /// <param name="id">The ID of the required Picture</param>
        public static string GetPicture(int id)
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "select bild from Bilder where bildId = " + id + " LIMIT 1"
            };
            ;
            string bild = command.ExecuteScalar<string>();
            return bild;
        }

        /// <summary>
        /// This method returns the name of the Icon from an Category as string
        /// </summary>
        /// <param name="id">The ID of the required Icon</param>
        public static string GetIconCat(int id)
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "select kategorieIcon from KategorieIcons where kategorieIconId = " + id + " LIMIT 1"
            };
            ;
            string icon = command.ExecuteScalar<string>();
            return icon;
        }

        /// <summary>
        /// This method returns the name of the Icon from an Language as string
        /// </summary>
        /// <param name="id">The ID of the required Icon</param>
        public static string GetIconLan(int id)
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "select sprachIcon from SprachIcons where sprachIconId = " + id + " LIMIT 1"
            };
            ;
            string icon = command.ExecuteScalar<string>();
            return icon;
        }

        /// <summary>
        /// This method returns the Vocab of given the id from the language and the id from the bairisch as String
        /// </summary>
        /// <param name="sprId">The ID of the target language</param>
        /// <param name="baiId">The ID of the given Bairisch</param>
        public static string GetTranslation(int sprId, int baiId)
        {
            var conn = manager.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(conn)
            {
                CommandText = "select vokabel from Uebersetzungssprachen where spracheId = " + sprId +
                " AND bairischId =  " + baiId + " LIMIT 1"
            };
            string vocab = command.ExecuteScalar<string>();
            return vocab;
        }
    }
}
