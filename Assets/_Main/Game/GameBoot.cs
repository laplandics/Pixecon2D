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
            SetServices();
            return SetGameObservable();
        }

        private void SetEssentials(Core.DI gameDi)
        {
            _gameDi = gameDi;
            _gameDi.Register(_ => new Utils.Cam("GameCam"));
            _gameDi.Resolve<Utils.Cam>().Instantiate();
            _gameDi.Resolve<Utils.UI>().AttachUI(Constant.Names.UI.GAME_UI, out _gameUI);
        }

        private void SetServices()
        {
            
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
                _gameDi.Resolve<Utils.UI>().DetachUI<GameUI>();
                _gameDi.Resolve<Utils.UI>().Clear();
            });
            
            return exitSignal;
        }
    }
}