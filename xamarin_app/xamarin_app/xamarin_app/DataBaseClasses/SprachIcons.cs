using SQLite;

namespace xamarin_app.DataBaseClasses
{
    class SprachIcons
    {
        [PrimaryKey]
        public int sprachIconId { get; set; }

        public string sprachIcon { get; set; }
    }
}
