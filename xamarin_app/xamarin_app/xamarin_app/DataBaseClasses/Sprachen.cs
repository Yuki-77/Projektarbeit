using SQLite;
using SQLiteNetExtensions.Attributes;

namespace xamarin_app.DataBaseClasses
{
    public class Sprachen
    {
        [PrimaryKey]
        public int spracheId { get; set; }

        public string sprache { get; set; }

        [ForeignKey(typeof(SprachIcons))]
        public int sprachIconId { get; set; }
    }
}
