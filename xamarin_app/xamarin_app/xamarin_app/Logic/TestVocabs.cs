using System.Collections.Generic;

namespace xamarin_app.Logic
{
    /// <summary>
    /// This class holds all informations about an view for the TestActivity
    /// </summary>
    public class TestVocabs
    {
        private readonly List<TestItem> pictures;
        private readonly string audio;

        public TestVocabs(List<TestItem> pictures, string audio)
        {
            this.pictures = pictures;
            this.audio = audio;
        }

        public List<TestItem> GetPictures() { return pictures; }

        public string GetAudio() { return audio; }
    }
}
