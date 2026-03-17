using R3;
using UnityEngine;

namespace Game
{
    public class GameBoot : MonoBehaviour
    {
        private Core.DI _gameDi;
        private GameUI _gameUI;
    
        public Observable<GameExitParams> Boot(Core.DI gameDi, GameEntryParams entryParams)
        {
            SetEssentials(gameDi);
            return SetGameObservable();
        }

        private void SetEssentials(Core.DI gameDi)
        {
            _gameDi = gameDi;
            _gameDi.Register(_ => new Utils.Cam("GameCam"));
            _gameDi.Resolve<Utils.Cam>().Instantiate();
            _gameDi.Resolve<Utils.UI>().New(Utils.UI.GAME_UI);
            _gameDi.Resolve<Utils.UI>().AttachSceneUI(out _gameUI);
        }

        private Observable<GameExitParams> SetGameObservable()
        {
            var exitSignalSubject = new Subject<Unit>();
            _gameUI.Bind(exitSignalSubject);
            
            var menuEntryState = new Menu.MenuEntryParams("");
            var exitState = new GameExitParams(menuEntryState);
            var exitSignal = exitSignalSubject.Select(_ => exitState);
            exitSignal.Subscribe(_ =>
            {
                _gameDi.Resolve<Utils.UI>().RemoveSceneUI<GameUI>();
                _gameDi.Resolve<Utils.UI>().Clear();
            });
            
            return exitSignal;
        }
    }
}