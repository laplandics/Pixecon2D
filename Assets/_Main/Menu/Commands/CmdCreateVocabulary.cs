using Cmd;
using R3;

namespace Menu
{
    public class CmdCreateVocabulary : ICommand
    {
        public readonly string VocabularyTitle;
        public readonly bool IsDone;
        public readonly bool IsIncluded;
        
        public CmdCreateVocabulary(string vocabularyTitle, bool isDone = false, bool isIncluded = true)
        {
            VocabularyTitle = vocabularyTitle;
            IsDone = isDone;
            IsIncluded = isIncluded;
        }
    }
}