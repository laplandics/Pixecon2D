using System;

namespace Data
{
    [Serializable]
    public class VocabularyEntryData : EntityData
    {
        public string word;
        public string translation;
        public bool isDone;
        public bool isCurrent;
    }
}