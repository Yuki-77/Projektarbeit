using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;

namespace xamarin_app
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        // true means system sound is on
        private const string IdSound = "sound";
        private static readonly bool soundDefault = true;
        public static bool Sound
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdSound, soundDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdSound, value);
            }
        }

        //-1 means no language selected (default)
        private const string IdLanguage = "language";
        private static readonly int languageDefault = -1;
        public static int Language
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdLanguage, languageDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdLanguage, value);
            }
        }

        //saves which Categorys are finished learning
        private const string IdCatFinishedLearn = "catFinishedLearn";
        public static List<int> CatFinishedLearn
        {
            get
            {
                string value = AppSettings.GetValueOrDefault(IdCatFinishedLearn, string.Empty);
                var lst = new List<int>();
                if (!string.IsNullOrEmpty(value))
                    lst = JsonConvert.DeserializeObject<List<int>>(value);
                return lst;
            }
            set
            {
                string lstValue = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(IdCatFinishedLearn, lstValue);
            }
        }

        //saves which Categorys are finished testing
        private const string IdCatFinishedTest = "catFinishedTest";
        public static List<int> CatFinishedTest
        {
            get
            {
                string value = AppSettings.GetValueOrDefault(IdCatFinishedTest, string.Empty);
                var lst = new List<int>();
                if (!string.IsNullOrEmpty(value))
                    lst = JsonConvert.DeserializeObject<List<int>>(value);
                return lst;
            }
            set
            {
                string lstValue = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(IdCatFinishedTest, lstValue);
            }
        }
    }
}


