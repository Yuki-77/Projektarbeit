using System;
using System.Collections.Generic;
using xamarin_app.DataBaseClasses;

namespace xamarin_app.Logic
{
    /// <summary>
    /// This class holds all Methods for the Learn- and TestActicity
    /// </summary>
    public class LearnTest
    {
        public readonly List<Bairisch> lst;
        public readonly int catId;
        private int count;

        /// <summary>
        /// This class defines functions for going through a Chapter
        /// </summary>
        /// <param name="catId">The ID of the required Kapitel</param>
        public LearnTest(int catId)
        {
            lst = Querys.GetItems(catId);
            count = 0;
            this.catId = catId;
        }

        /// <summary>
        /// This method retuns the actual object
        /// </summary>
        public Bairisch GetActualObject() { return lst[count]; }

        /// <summary>
        /// This method retuns the actual position of the pointer
        /// </summary>
        public int GetCount() { return count; }

        /// <summary>
        /// This method returns the number of vocabs
        /// </summary>
        public int Size()
        {
            int result = 0;
            int tmp = 0;
            while (tmp < lst.Count)
            {
                if (IsLegit(lst[tmp])) result++;
                tmp++;
            }
            return result;
        }

        /// <summary>
        /// This method returns if the Kapitel has a next legit item
        /// </summary>
        public bool HasNext()
        {
            int tmp = count + 1;
            while (tmp + 1 <= lst.Count)
            {
                if (IsLegit(lst[tmp])) return true;
                else tmp++;
            }
            return false;
        }

        /// <summary>
        /// This method returns the path of the picture of the actual vocab
        /// </summary>
        public string GetPicture()
        {
            var path = Querys.GetPicture(lst[count].bildId);
            return path;
        }

        /// <summary>
        /// This method returns the path of the audio of the actual vocab
        /// </summary>
        public string GetAudio()
        {
            var path = Querys.GetAudio(lst[count].ausspracheId);
            return path;
        }

        /// <summary>
        /// This method puts the pointer to the next item
        /// </summary>
        public void Next()
        {
            do count++; while (!IsLegit(lst[count]));
        }

        /// <summary>
        /// This method puts the pointer to the previous item
        /// </summary>
        public void Previous()
        {
            do count--; while (!IsLegit(lst[count]));
        }

        /// <summary>
        /// This Method returns if a given item is legit (has a translation, name and picture)
        /// </summary>
        /// <param name="item">The item that should be tested</param>
        public static bool IsLegit(Bairisch item)
        {
            if (
                String.IsNullOrEmpty(item.vokabel) ||
                String.IsNullOrEmpty(Querys.GetAudio(item.ausspracheId)) ||
                String.IsNullOrEmpty(Querys.GetTranslation(1, item.bairischId)) ||
                String.IsNullOrEmpty(Querys.GetTranslation(Settings.Language, item.bairischId)) ||
                String.IsNullOrEmpty(Querys.GetPicture(item.bildId))
                )
            {
                Console.WriteLine("\t\t*****ERROR*****\nVocab " + item.bairischId + ": " + item.vokabel + " is invalid.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// This Method sets the state of the active Kapitel to executed
        /// Should be called right before starting the Congrats-Activity
        /// </summary>
        public virtual void SetExecution() { }
    }
}
