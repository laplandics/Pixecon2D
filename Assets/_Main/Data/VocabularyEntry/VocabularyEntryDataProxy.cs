using R3;

namespace Proxy
{
    public class VocabularyEntryDataProxy
    {
        public int Id { get; }
        public Data.VocabularyEntryData Origin { get; }
        
        public ReactiveProperty<string> Word { get; }
        public ReactiveProperty<string> Translation { get; }
        public ReactiveProperty<bool> IsDone { get; }
        public ReactiveProperty<bool> IsCurrent { get; }

        public ReactiveProperty<char> LastEnteredLetter { get; }
        
        public VocabularyEntryDataProxy(Data.VocabularyEntryData origin)
        {
            Origin = origin;
            Id = origin.entityID;
            
            Word = new ReactiveProperty<string>(origin.word);
            Translation = new ReactiveProperty<string>(origin.translation);
            IsDone = new ReactiveProperty<bool>(origin.isDone);
            IsCurrent = new ReactiveProperty<bool>(origin.isCurrent);
            LastEnteredLetter = new ReactiveProperty<char>();
            
            Word.Skip(1).Subscribe(word => origin.word = word);
            Translation.Skip(1).Subscribe(translation => origin.translation = translation);
            IsDone.Skip(1).Subscribe(isDone => origin.isDone = isDone);
            IsCurrent.Skip(1).Subscribe(isCurrent => origin.isCurrent = isCurrent);
        }
    }
}