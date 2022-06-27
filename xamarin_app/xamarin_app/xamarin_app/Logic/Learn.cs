using xamarin_app.Logic;

namespace xamarin_app.Query
{
    /// <summary>
    /// This class holds all Methods for the LearnActicity
    /// </summary>
    public sealed class Learn : LearnTest
    {
        public Learn(int catId) : base(catId) { }

        /// <summary>
        /// This Method returns the Vocab
        /// </summary>
        public string GetVocab()
        {
            return lst[GetCount()].vokabel;
        }

        /// <summary>
        /// This Method returns Bairisch
        /// </summary>
        public string GetWord()
        {
            return lst[GetCount()].woertlich;
        }

        /// <summary>
        /// This Method returns the Comment
        /// </summary>
        public string GetComment()
        {
            return lst[GetCount()].bemerkung;
        }

        /// <summary>
        /// This Method returns the Vocab in Hochdeutsch
        /// </summary>
        public string GetStandardGerman()
        {
            return Querys.GetTranslation(1, lst[GetCount()].bairischId);
        }

        /// <summary>
        /// This Method returns the selected Translation
        /// </summary>
        public string GetTranslation()
        {
            if (Settings.Language == 1) return "";
            return Querys.GetTranslation(Settings.Language, lst[GetCount()].bairischId);
        }

        /// <summary>
        /// This Method sets the state of the active Kapitel to executed
        /// Should be called right before starting the Congrats-Activity
        /// </summary>
        public override void SetExecution()
        {
            var exec = Settings.CatFinishedLearn;
            if (!exec.Contains(catId))
            {
                exec.Add(catId);
                Settings.CatFinishedLearn = exec;
            }
        }
    }
}
