using System;
using System.Collections.Generic;
using xamarin_app.Logic;

namespace xamarin_app.Query
{
    public static class Categories
    {

        /// <summary>
        /// This Method returns true if the given Category is executed
        /// </summary>
        /// <param name="cat">The ID of the required Category</param>
        private static bool IsExecutedLearn(int cat)
        {
            var exec = Settings.CatFinishedLearn;
            if (exec.Contains(cat)) return true;
            return false;
        }

        /// <summary>
        /// This Method returns true if the given Category is executed
        /// </summary>
        /// <param name="cat">The ID of the required Category</param>
        private static bool IsExecutedTest(int cat)
        {
            var exec = Settings.CatFinishedTest;
            if (exec.Contains(cat)) return true;
            return false;
        }

        /// <summary>
        /// This Method returns the number of Categories
        /// </summary>
        public static int AnzCategories()
        {
            return Querys.GetCategories().Count;
        }

        /// <summary>
        /// This Method returns all Categories as List of Category
        /// </summary>
        /// <param name="learn">Should be true for learn and false for test</param>
        public static List<Choose> GetCategories(bool learn)
        {
            List<DataBaseClasses.Kategorie> section = Querys.GetCategories();
            List<Choose> categories = new List<Choose>();
            Choose tmp;
            foreach (DataBaseClasses.Kategorie cat in section)
            {
                string icon = Querys.GetIconCat(cat.kategorieIconId);
                if (learn)
                {
                    tmp = new Choose(cat.name, icon, cat.kategorieId, IsExecutedLearn(cat.kategorieId));
                }
                else
                {
                    tmp = new Choose(cat.name, icon, cat.kategorieId, IsExecutedTest(cat.kategorieId));
                }
                if (IsLegit(tmp)) categories.Add(tmp);
                else
                {
                    Console.WriteLine("\t\t*****ERROR*****\nCategory " + cat.kategorieId + " is invalid.");
                }
            }
            return categories;
        }

        /// <summary>
        /// This Method returns if a given Category has a name and an icon
        /// </summary>
        /// <param name="cat">The Category which should be tested</param>
        public static bool IsLegit(Choose cat)
        {
            if (String.IsNullOrEmpty(cat.GetIcon()) || String.IsNullOrEmpty(cat.GetName())) return false;
            return true;
        }
    }
}
