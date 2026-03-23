using Cmd;
using ObservableCollections;
using Proxy;

namespace Game
{
    public class CmdChangeCurrentTranslationHandler : ICommandHandler<CmdChangeCurrentTranslation>
    {
        private readonly IObservableCollection<VocabularyDataProxy> _vocabularies;

        public CmdChangeCurrentTranslationHandler(IObservableCollection<VocabularyDataProxy> vocabularies)
        {
            _vocabularies = vocabularies;
        }
        
        public bool Handle(CmdChangeCurrentTranslation command)
        {
            var vocabDone = true;
            foreach (var vocabulary in _vocabularies)
            {
                if (vocabulary.IsCompleted.Value) continue;
                foreach (var entry in vocabulary.VocabularyEntries)
                {
                    if (entry.IsDone.Value) continue;
                    if (entry.IsCurrent.Value)
                    { entry.IsCurrent.Value = false; continue; }
                    
                    vocabDone = false;
                    entry.IsCurrent.Value = true;
                    break;
                }
                if (!vocabDone) return true;
                vocabulary.IsCompleted.Value = true;
            }
            
            return false;
        }
    }
}