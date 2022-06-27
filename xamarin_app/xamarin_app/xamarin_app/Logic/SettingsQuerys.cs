using System.Collections.Generic;
using xamarin_app.DataBaseClasses;

namespace xamarin_app.Logic
{
    /// <summary>
    /// This class has Methods for chahinging the Settings
    /// </summary>
    public static class SettingsQuerys
    {

        /// <summary>
        /// This method resets the progress in learn and test
        /// </summary>
        public static void Reset()
        {
            Settings.CatFinishedLearn = new List<int>();
            Settings.CatFinishedTest = new List<int>();
        }

        /// <summary>
        /// This method returns if the system sound is on
        /// </summary>
        public static bool IsSoundOn()
        {
            return Settings.Sound;
        }

        /// <summary>
        /// This method sets the system sound on/off
        /// </summary>
        /// <param name="on">true for on and false for off</param>
        public static void SetSound(bool on)
        {
            Settings.Sound = on;
        }

        /// <summary>
        /// This method returns all Languages as List of Choose
        /// </summary>
        public static List<Choose> GetLanguages()
        {
            List<Choose> ret = new List<Choose>();
            List<Sprachen> tmp = Querys.GetLanguages();
            string icon;
            foreach (Sprachen sprache in tmp)
            {
                icon = Querys.GetIconLan(sprache.sprachIconId);
                ret.Add(new Choose(sprache.sprache, icon, sprache.spracheId, false));
            }
            ret.Sort();
            return ret;
        }

        /// <summary>
        /// This method sets the language in the Settings
        /// </summary>
        /// <param name="spracheId">The ID of the selected Language</param>
        public static void SetLanguage(int spracheId)
        {
            Settings.Language = spracheId;
        }

        /// <summary>
        /// This method returns false if the Language is not set yet
        /// </summary>
        public static bool IsLanguageSet()
        {
            if (Settings.Language == -1) return false;
            else return true;
        }
    }
}
