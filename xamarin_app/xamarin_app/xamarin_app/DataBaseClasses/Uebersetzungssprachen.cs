using SQLite;
using SQLiteNetExtensions.Attributes;

namespace xamarin_app.DataBaseClasses
{
    public class Uebersetzungssprachen
    {
        [PrimaryKey]
        public int uebersetzungsspracheId { get; set; }

        [ForeignKey(typeof(Sprachen))]
        public int spracheId { get; set; }

        public string vokabel { get; set; }

        [ForeignKey(typeof(Bairisch))]
        public int bairischId { get; set; }
    }
}
