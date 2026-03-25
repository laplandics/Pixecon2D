using Game;
using ObservableCollections;

namespace GameView
{
    public class GameUIRootViewModel
    {
        public readonly CurrentVocabularyHandler VocabularyHandler;
        public readonly GameCycleHandler CycleHandler;
        public readonly ObservableList<PopupViewModel> AllPopups;
        
        public GameUIRootViewModel(GameCycleHandler cycleHandler, GamePopupHandler popupHandler,
            CurrentVocabularyHandler vocabularyHandler)
        {
            VocabularyHandler = vocabularyHandler;
            CycleHandler = cycleHandler;
            AllPopups = popupHandler.AllPopups;
        }
    }
}