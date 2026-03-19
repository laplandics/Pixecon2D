using Cmd;
using R3;

namespace Menu
{
    public class CmdCreateVocabulary : ICommand
    {
        public readonly string VocabularyTitle;
        public readonly bool IsDone;
        public readonly bool IsIncluded;

        public ReactiveProperty<Proxy.VocabularyDataProxy> Result { get; }
        
        public CmdCreateVocabulary(string vocabularyTitle, ReactiveProperty<Proxy.VocabularyDataProxy> result = null,
            bool isDone = false, bool isIncluded = true)
        {
            VocabularyTitle = vocabularyTitle;
            IsDone = isDone;
            IsIncluded = isIncluded;
            Result = result;
        }
    }
}