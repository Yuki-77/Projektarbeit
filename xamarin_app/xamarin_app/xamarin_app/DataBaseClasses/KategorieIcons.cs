using SQLite;

namespace xamarin_app.DataBaseClasses
{
    public class KategorieIcons
    {
        [PrimaryKey]
        public int kategorieIconId { get; set; }

        public string kategorieIcon { get; set; }
    }
}
