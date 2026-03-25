using R3;

namespace Proxy
{
    public class VocabularyEntryDataProxy
    {
        public int Id { get; }
        public Data.VocabularyEntryData Origin { get; }
        
        public ReactiveProperty<string> Word { get; }
        public ReactiveProperty<string> Translation { get; }
        public ReactiveProperty<bool> IsCompleted { get; }
        
        public VocabularyEntryDataProxy(Data.VocabularyEntryData origin)
        {
            Origin = origin;
            Id = origin.entityID;
            
            Word = new ReactiveProperty<string>(origin.word);
            Translation = new ReactiveProperty<string>(origin.translation);
            IsCompleted = new ReactiveProperty<bool>(origin.isCompleted);
            
            Word.Skip(1).Subscribe(word => origin.word = word);
            Translation.Skip(1).Subscribe(translation => origin.translation = translation);
        }
    }
}