using Cmd;
using R3;

namespace Menu
{
    public class CmdCreateVocabularyEntry : ICommand
    {
        public readonly string Word;
        public readonly string Translation;
        public readonly bool IsDone;
        
        public ReactiveProperty<Proxy.VocabularyEntryDataProxy> Result { get; }
        
        public CmdCreateVocabularyEntry(string word, string translation,
            ReactiveProperty<Proxy.VocabularyEntryDataProxy> result, bool isDone = false)
        {
            Word = word;
            Translation = translation;
            IsDone = isDone;
            Result = result;
        }
    }
}