using System.Linq;
using ObservableCollections;
using R3;

namespace Proxy
{
    public class ProjectDataProxy
    {
        private readonly Data.ProjectData _origin;
        
        public ProjectDataProxy(Data.ProjectData origin)
        {
            _origin = origin;
            origin.vocabularies.ForEach(vocabularyData =>
                Vocabularies.Add(new VocabularyDataProxy(vocabularyData)));
            SubscribeToVocabulariesChange();
        }

        private void SubscribeToVocabulariesChange()
        {
            Vocabularies.ObserveAdd().Subscribe(addEvent =>
            {
                var addedVocabularyDataProxy = addEvent.Value;
                _origin.vocabularies.Add(addedVocabularyDataProxy.Origin);
            });

            Vocabularies.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedVocabularyDataProxy = removeEvent.Value;
                var removedVocabularyData = _origin.vocabularies.FirstOrDefault(vocabulary =>
                    vocabulary.entityID == removedVocabularyDataProxy.Id);
                _origin.vocabularies.Remove(removedVocabularyData);
            });
        }

        public int GetGlobalEntityId => _origin.globalEntityId++;
        public ObservableList<VocabularyDataProxy> Vocabularies { get; } = new();
    }
}