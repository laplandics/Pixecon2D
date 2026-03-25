using Cmd;
using ObservableCollections;
using Proxy;

namespace Game
{
    public class CmdChangeCurrentVocabularyHandler : ICommandHandler<CmdChangeCurrentVocabulary>
    {
        private readonly ObservableList<VocabularyDataProxy> _vocabularies;

        public CmdChangeCurrentVocabularyHandler(ObservableList<VocabularyDataProxy> vocabularies)
        {
            _vocabularies = vocabularies;
        }
        
        public bool Handle(CmdChangeCurrentVocabulary command)
        {
            var currentVocab = command.CurrentVocabulary;
            currentVocab.IsCompleted.Value = true;
            
            var currentVocabIndex = _vocabularies.IndexOf(currentVocab);
            return currentVocabIndex < _vocabularies.Count - 1;
        }
    }
}