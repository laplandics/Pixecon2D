using System.Linq;
using ObservableCollections;
using R3;

namespace Proxy
{
    public class ProjectDataProxy
    {
        public ObservableList<VocabularyDataProxy> Vocabularies { get; } = new();
        
        public ProjectDataProxy(Data.ProjectData origin)
        {
            origin.vocabularies.ForEach(vocabularyData =>
                Vocabularies.Add(new VocabularyDataProxy(vocabularyData)));
            SubscribeToVocabulariesChange(origin);
        }

        private void SubscribeToVocabulariesChange(Data.ProjectData origin)
        {
            Vocabularies.ObserveAdd().Subscribe(addEvent =>
            {
                var addedVocabularyDataProxy = addEvent.Value;
                origin.vocabularies.Add(addedVocabularyDataProxy.Origin);
            });

            Vocabularies.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedVocabularyDataProxy = removeEvent.Value;
                var removedVocabularyData = origin.vocabularies.FirstOrDefault(vocabulary =>
                    vocabulary.id == removedVocabularyDataProxy.Id);
                origin.vocabularies.Remove(removedVocabularyData);
            });
        }
    }
}