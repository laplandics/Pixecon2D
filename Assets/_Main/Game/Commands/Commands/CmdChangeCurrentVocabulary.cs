using Cmd;
using Proxy;

namespace Game
{
    public class CmdChangeCurrentVocabulary : ICommand
    {
        public readonly VocabularyDataProxy CurrentVocabulary;

        public CmdChangeCurrentVocabulary(VocabularyDataProxy currentVocabulary)
        {
            CurrentVocabulary = currentVocabulary;
        }
    }
}