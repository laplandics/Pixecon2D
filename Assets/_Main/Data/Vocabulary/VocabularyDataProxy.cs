using System.Linq;
using ObservableCollections;
using R3;

namespace Proxy
{
    public class VocabularyDataProxy
    {
        public int Id { get; }
        public string Key { get; }
        public Data.VocabularyData Origin { get; }
        
        public ReactiveProperty<string> Title { get; }
        public ReactiveProperty<bool> IsCompleted { get; }
        public ObservableList<VocabularyEntryDataProxy> VocabularyEntries { get; } = new();

        public VocabularyDataProxy(Data.VocabularyData origin)
        {
            Origin = origin;
            Id = origin.entityID;
            Key = origin.key;
            
            Title = new ReactiveProperty<string>(origin.title);
            IsCompleted = new ReactiveProperty<bool>(origin.isCompleted);
            origin.vocabularyEntries.ForEach(e => VocabularyEntries.Add(new VocabularyEntryDataProxy(e)));
            
            Title.Skip(1).Subscribe(title => origin.title = title);
            IsCompleted.Skip(1).Subscribe(isCompleted => origin.isCompleted = isCompleted);
            SubscribeToVocabularyEntryChange(origin);
        }

        private void SubscribeToVocabularyEntryChange(Data.VocabularyData origin)
        {
            VocabularyEntries.ObserveAdd().Subscribe(addEvent =>
            {
                var addedVocabularyEntryDataProxy = addEvent.Value;
                origin.vocabularyEntries.Add(addedVocabularyEntryDataProxy.Origin);
            });

            VocabularyEntries.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedVocabularyEntryDataProxy = removeEvent.Value;
                var removedVocabularyEntryData = origin.vocabularyEntries.FirstOrDefault(entry =>
                    entry.entityID == removedVocabularyEntryDataProxy.Id);
                origin.vocabularyEntries.Remove(removedVocabularyEntryData);
            });
        }
    }
}