using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ProjectData
    {
        public List<VocabularyData> vocabularies = new();
        public List<VocabularyEntryData> vocabulariesEntries = new();
    }
}