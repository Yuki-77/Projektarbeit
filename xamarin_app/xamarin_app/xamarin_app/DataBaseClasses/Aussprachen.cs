using SQLite;

namespace xamarin_app.DataBaseClasses
{
    public class Aussprachen
    {
        [PrimaryKey]
        public int ausspracheId { get; set; }
        public string aussprache { get; set; }
    }
}
