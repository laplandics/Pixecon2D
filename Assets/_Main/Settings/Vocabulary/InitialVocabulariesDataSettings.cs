using System;

namespace Settings
{
    [Serializable]
    public class InitialVocabulariesDataSettings
    {
        public InitialVocabularyData[] initialVocabulariesData;
    }

    [Serializable]
    public class InitialVocabularyData
    {
        public string key;
        public string title;
        public VocabularyEntryInitialData[] entries;

        [Serializable]
        public class VocabularyEntryInitialData
        {
            public string word;
            public string translation;
            public bool isInitial;
        }
    }
}