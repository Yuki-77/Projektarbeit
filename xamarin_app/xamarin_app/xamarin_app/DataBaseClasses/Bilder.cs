using SQLite;

namespace xamarin_app.DataBaseClasses
{
    public class Bilder
    {
        [PrimaryKey]
        public int bildId { get; set; }

        public string bild { get; set; }
    }
}
