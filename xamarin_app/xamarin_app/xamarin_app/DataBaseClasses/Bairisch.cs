using SQLite;
using SQLiteNetExtensions.Attributes;

namespace xamarin_app.DataBaseClasses
{
    public class Bairisch
    {
        [PrimaryKey]
        public int bairischId { get; set; }

        [ForeignKey(typeof(Kategorie))]
        public int kategorieId { get; set; }

        public string vokabel { get; set; }

        public string woertlich { get; set; }

        public string bemerkung { get; set; }

        [ForeignKey(typeof(Bilder))]
        public int bildId { get; set; }

        [ForeignKey(typeof(Aussprachen))]
        public int ausspracheId { get; set; }
    }
}
