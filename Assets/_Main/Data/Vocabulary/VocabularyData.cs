using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class VocabularyData : EntityData
    {
        public string key;
        
        public string title;
        public bool isCompleted;
        public List<VocabularyEntryData> vocabularyEntries;
    }
}