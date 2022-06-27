using SQLite;
using System;
using System.IO;
using xamarin_app.Helpers;

namespace xamarin_app.iOS.Model
{
    class IOSSQLite : ISQLite
    {
        public SQLiteConnection GetConnection(string dbName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder  
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder  
            var path = Path.Combine(libraryPath, dbName);
            // Create the connection 
            var connection = new SQLiteConnection(path);
            // Return the database connection  
            return connection;
        }
    }
}