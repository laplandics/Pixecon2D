using R3;
using UnityEngine;

namespace Menu
{
    public class MenuBoot : MonoBehaviour
    {
        private Core.DI _menuDi;
        private MenuUI _menuUI;
        
        public Observable<MenuExitParams> Boot(Core.DI menuDi, MenuEntryParams entryParams)
        {
            SetEssentials(menuDi);
            return SetMenuObservable();
        }

        private void SetEssentials(Core.DI menuDi)
        {
            _menuDi = menuDi;
            _menuDi.Register(_ => new Utils.Cam("MenuCam"));
            _menuDi.Resolve<Utils.Cam>().Instantiate();
            _menuDi.Resolve<Utils.UI>().New(Utils.UI.MENU_UI);
            _menuDi.Resolve<Utils.UI>().AttachSceneUI(out _menuUI);
        }

        private Observable<MenuExitParams> SetMenuObservable()
        {
            var playSignalSubject = new Subject<Unit>();
            _menuUI.Bind(playSignalSubject);

            var gameEntryState = new Game.GameEntryParams();
            var exitState = new MenuExitParams(gameEntryState);
            var exitSignal = playSignalSubject.Select(_ => exitState);
            exitSignal.Subscribe(_ =>
            {
                _menuDi.Resolve<Utils.UI>().RemoveSceneUI<MenuUI>();
                _menuDi.Resolve<Utils.UI>().Clear();
            });
            
            return exitSignal;
        }
    }
}