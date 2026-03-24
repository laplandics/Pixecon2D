using Game;
using ObservableCollections;
using Proxy;

namespace GameView
{
    public class GameUIRootViewModel
    {
        public readonly GameCycleHandler CycleHandler;
        public readonly ObservableList<VocabularyEntryDataProxy> VocabularyEntries;
        public readonly ObservableList<PopupViewModel> AllPopups;
        
        public GameUIRootViewModel(GameCycleHandler cycleHandler, 
            TranslationChanger translationChanger,
            GamePopupHandler popupHandler)
        {
            CycleHandler = cycleHandler;
            AllPopups = popupHandler.AllPopups;
            VocabularyEntries = translationChanger.AllEntries;
        }
    }
}