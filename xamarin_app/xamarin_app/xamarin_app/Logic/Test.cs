using System;
using System.Collections.Generic;
using xamarin_app.DataBaseClasses;
using xamarin_app.Logic;

namespace xamarin_app.Query
{
    /// <summary>
    /// This class holds all Methods for the TestActicity
    /// </summary>
    public sealed class Test : LearnTest
    {

        private static Random rng = new Random();

        public Test(int catId) : base(catId) { }

        /// <summary>
        /// This Method sets the state of the active Category to executed
        /// Should be called right before starting the Congrats-Activity
        /// </summary>
        /// <param name="kat">The ID of the required Category</param>
        public static void SetExecution(int catId)
        {
            var exec = Settings.CatFinishedTest;
            if (!exec.Contains(catId))
            {
                exec.Add(catId);
                Settings.CatFinishedTest = exec;
            }
        }

        /// <summary>
        /// This method returns all Items for the TestActivity
        /// </summary>
        /// <param name="validItems">The List of valid items</param>
        public static List<TestVocabs> GetAllItems(List<Bairisch> validItems)
        {
            List<TestItem> other;
            List<Bairisch> tmp;
            List<TestVocabs> ret = new List<TestVocabs>();
            foreach (Bairisch item in validItems)
            {
                other = new List<TestItem>();
                tmp = new List<Bairisch>(validItems);
                string vocab = Querys.GetTranslation(Settings.Language, item.bairischId);
                string picture = Querys.GetPicture(item.bildId);
                TestItem newItem = new TestItem(picture, vocab, true);
                other.Add(newItem);
                tmp.Remove(item);
                Random cnt = new Random();
                int random;
                bool equalsOther;
                while (other.Count < 4)
                {
                    random = cnt.Next(0, tmp.Count);
                    equalsOther = false;
                    vocab = Querys.GetTranslation(Settings.Language, tmp[random].bairischId);
                    for (int i = 0; i < other.Count; i++)
                    {
                        if (vocab.Equals(other[i].GetVocab())) equalsOther = true;
                    }
                    if (!equalsOther)
                    {
                        picture = Querys.GetPicture(tmp[random].bildId);
                        newItem = new TestItem(picture, vocab, false);
                        other.Add(newItem);
                    }
                    tmp.Remove(tmp[random]);
                }
                random = cnt.Next(0, 4);
                Swap(other, 0, random);
                ret.Add(new TestVocabs(other, Querys.GetAudio(item.ausspracheId)));
            }
            Shuffle(ret);
            return ret;
        }

        static void Swap(IList<TestItem> list, int indexA, int indexB)
        {
            TestItem tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        static void Shuffle(List<TestVocabs> lst)
        {
            int n = lst.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                TestVocabs value = lst[k];
                lst[k] = lst[n];
                lst[n] = value;
            }
        }
    }
}
