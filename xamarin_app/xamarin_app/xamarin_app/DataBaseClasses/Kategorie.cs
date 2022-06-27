using SQLite;
using SQLiteNetExtensions.Attributes;

namespace xamarin_app.DataBaseClasses
{
    public class Kategorie
    {
        [PrimaryKey]
        public int kategorieId { get; set; }

        public string name { get; set; }

        [ForeignKey(typeof(KategorieIcons))]
        public int kategorieIconId { get; set; }
    }
}
