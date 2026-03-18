using R3;

namespace Proxy
{
    public class VocabularyEntryDataProxy
    {
        public int Id { get; }
        public string Key { get; }
        public Data.VocabularyEntryData Origin { get; }
        
        public ReactiveProperty<string> Word { get; }
        public ReactiveProperty<string> Translation { get; }
        public ReactiveProperty<bool> IsDone { get; }

        public VocabularyEntryDataProxy(Data.VocabularyEntryData origin)
        {
            Origin = origin;
            Id = origin.id;
            Key = origin.key;
            
            Word = new ReactiveProperty<string>(origin.word);
            Translation = new ReactiveProperty<string>(origin.translation);
            IsDone = new ReactiveProperty<bool>(origin.isDone);
            
            Word.Skip(1).Subscribe(word => origin.word = word);
            Translation.Skip(1).Subscribe(translation => origin.translation = translation);
            IsDone.Skip(1).Subscribe(isDone => origin.isDone = isDone);
        }
    }
}