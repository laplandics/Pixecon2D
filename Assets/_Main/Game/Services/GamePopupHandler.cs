using System.Linq;
using GameView;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class GamePopupHandler
    {
        private readonly GameCycleHandler _gameCycleHandler;
        private readonly IObservableCollection<VocabularyDataProxy> _vocabularies;
        
        public ObservableList<PopupViewModel> AllPopups { get; } = new();

        public GamePopupHandler(GameCycleHandler gameCycleHandler,
            IObservableCollection<VocabularyDataProxy> vocabularies)
        {
            _gameCycleHandler = gameCycleHandler;
            _vocabularies = vocabularies;
           
            foreach (var vocab in vocabularies)
            { vocab.IsCompleted.Skip(1).Subscribe( _ =>
                { if (_vocabularies.FirstOrDefault(v => !v.IsCompleted.Value) != null) return;
                    CreatePopup(Constant.Names.UI.GAME_NO_VOCABULARIES_POPUP); });}
        }
        
        private void CreatePopup(string popupPath)
        {
            if (_vocabularies.FirstOrDefault(v => !v.IsCompleted.Value) != null) return;
            AllPopups.Add(new PopupViewModel(popupPath, _gameCycleHandler));
        }
    }
}