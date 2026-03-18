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
            SetServices(menuDi);
            ResolveEssentials();
            return SetMenuObservable();
        }

        private void SetServices(Core.DI menuDi)
        {
            _menuDi = menuDi;
            _menuDi.Register(_ => new Utils.Cam("MenuCam"));
            _menuDi.Register(_ => new MenuUiInteractor(
                _menuDi.Resolve<Utils.UI>(),
                _menuDi.Resolve<IProjectDataProvider>().ProjectData));
        }
        
        private void ResolveEssentials()
        {
            _menuDi.Resolve<Utils.Cam>().Instantiate();
            _menuUI = _menuDi.Resolve<MenuUiInteractor>().Instantiate();
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
                _menuDi.Resolve<Utils.UI>().DetachUI<MenuUI>();
                _menuDi.Resolve<Utils.UI>().Clear();
            });
            
            return exitSignal;
        }
    }
}