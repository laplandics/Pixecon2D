using System;

namespace Data
{
    [Serializable]
    public class VocabularyEntryData : EntityData
    {
        public string key;
        
        public string word;
        public string translation;
        public bool isDone;
    }
}