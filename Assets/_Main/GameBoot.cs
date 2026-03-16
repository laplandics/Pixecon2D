using R3;
using UnityEngine;

namespace Game
{
    public class GameBoot : MonoBehaviour
    {
        private GameUI _gameUI;
    
        public Observable<GameExitState> Boot(GameEntryState entryState)
        {
            Utils.Cam.Instantiate();
            Utils.UI.New(Utils.UI.GAME_UI);
            Utils.UI.AttachSceneUI(out _gameUI);
            return GameSceneState();
        }

        private Observable<GameExitState> GameSceneState()
        {
            var exitSignalSubject = new Subject<Unit>();
            _gameUI.Bind(exitSignalSubject);
            
            var menuEntryState = new Menu.MenuEntryState("");
            var exitState = new GameExitState(menuEntryState);
            var exitSignal = exitSignalSubject.Select(_ => exitState);
            exitSignal.Subscribe(_ => {Utils.UI.RemoveSceneUI<GameUI>(); Utils.UI.Clear();});
            return exitSignal;
        }
    }
}