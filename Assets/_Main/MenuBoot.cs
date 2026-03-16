using R3;
using UnityEngine;

namespace Menu
{
    public class MenuBoot : MonoBehaviour
    {
        private MenuUI _menuUI;
        
        public Observable<MenuExitState> Boot(MenuEntryState entryState)
        {
            Utils.Cam.Instantiate();
            Utils.UI.New(Utils.UI.MENU_UI);
            Utils.UI.AttachSceneUI(out _menuUI);
            return MenuSceneState();
        }

        private Observable<MenuExitState> MenuSceneState()
        {
            var playSignalSubject = new Subject<Unit>();
            _menuUI.Bind(playSignalSubject);

            var gameEntryState = new Game.GameEntryState();
            var exitState = new MenuExitState(gameEntryState);
            var exitSignal = playSignalSubject.Select(_ => exitState);
            exitSignal.Subscribe(_ => {Utils.UI.RemoveSceneUI<MenuUI>(); Utils.UI.Clear();});
            return exitSignal;
        }
    }
}
