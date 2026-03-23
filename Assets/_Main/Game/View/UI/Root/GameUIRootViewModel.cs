using Game;
using ObservableCollections;
using Proxy;

namespace GameView
{
    public class GameUIRootViewModel
    {
        public readonly GameFinisher Finisher;
        public readonly ObservableList<VocabularyEntryDataProxy> VocabularyEntries;
        
        public GameUIRootViewModel(GameFinisher finisher, TranslationChanger translationChanger)
        {
            Finisher = finisher;
            VocabularyEntries = translationChanger.AllEntries;
        }
    }
}