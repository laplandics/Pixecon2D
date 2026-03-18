using System;

namespace Data
{
    [Serializable]
    public class VocabularyEntryData
    {
        public int id;
        public string key;
        
        public string word;
        public string translation;
        public bool isDone;
    }
}