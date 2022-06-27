using SQLite;
using System.IO;
using xamarin_app.Droid.Model;
using xamarin_app.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLite))]

namespace xamarin_app.Droid.Model
{
    public class AndroidSQLite : ISQLite
    {
        public SQLiteConnection GetConnection(string dbName)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Documents folder  
            var path = Path.Combine(documentsPath, dbName);
            var connection = new SQLiteConnection(path);

            // Return the database connection  
            return connection;
        }
    }
}