using SQLite;

namespace xamarin_app.Helpers
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(string dbName);
    }
}
