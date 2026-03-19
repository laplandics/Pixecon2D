using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ProjectData
    {
        public int globalEntityId;
        public List<VocabularyData> vocabularies = new();
    }
}