namespace xamarin_app.Logic
{
    /// <summary>
    /// This class holds all informations about an item for the TestActivity
    /// </summary>
    public class TestItem
    {
        private readonly string picture;
        private readonly string vocab;
        private readonly bool iconMatchesSound;

        public TestItem(string picture, string vocab, bool iconMatchesSound)
        {
            this.picture = picture;
            this.vocab = vocab;
            this.iconMatchesSound = iconMatchesSound;
        }

        public string GetPicture() { return picture; }

        public string GetVocab() { return vocab; }

        public bool GetIconMatchesSound() { return iconMatchesSound; }
    }
}
