using Cmd;
using Proxy;

namespace Game
{
    public class CmdChangeCurrentEntry : ICommand
    {
        public readonly VocabularyEntryDataProxy CurrentEntry;
        public readonly VocabularyDataProxy CurrentVocabulary;

        public CmdChangeCurrentEntry(VocabularyDataProxy currentVocabulary, VocabularyEntryDataProxy currentEntry)
        {
            CurrentEntry = currentEntry;
            CurrentVocabulary = currentVocabulary;
        }
    }
}