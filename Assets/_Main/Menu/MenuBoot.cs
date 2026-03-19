using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Menu
{
    public class MenuBoot : MonoBehaviour
    {
        private Core.DI _menuDi;
        private Subject<Unit> _playSignal;
        
        public Observable<MenuExitParams> Boot(Core.DI menuDi, MenuEntryParams entryParams)
        {
            SetEssentials(menuDi);
            return SetMenuObservable();
        }

        private void SetEssentials(Core.DI menuDi)
        {
            _menuDi = menuDi;
            _menuDi.Register(_ => new Utils.Cam("MenuCam"));
            _menuDi.Register<Cmd.ICommandProcessor>(_ => new Cmd.CommandProcessor(), true);

            _menuDi.Resolve<Utils.Cam>().Instantiate();
            _menuDi.Resolve<Cmd.ICommandProcessor>().RegisterHandler(new CmdCreateVocabularyHandler(
                _menuDi.Resolve<IProjectDataProvider>().ProjectData));
            _menuDi.Resolve<Cmd.ICommandProcessor>().RegisterHandler(new CmdCreateVocabularyEntryHandler(
                _menuDi.Resolve<IProjectDataProvider>().ProjectData));
            
            _menuDi.Register(_ => new VocabularyCreator(
                _menuDi.Resolve<Cmd.ICommandProcessor>(),
                _menuDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies));
            
            _playSignal = new Subject<Unit>();
            _menuDi.Register(_ => new MenuUiInteractor(
                _menuDi.Resolve<Utils.UI>(),
                _menuDi.Resolve<VocabularyCreator>(),
                new Dictionary<string, Subject<Unit>>
                { [MenuUiInteractor.PLAY_BUTTON_SIGNAL_NAME] = _playSignal}));
            
            _menuDi.Resolve<MenuUiInteractor>().Instantiate();
            
        }
        
        private Observable<MenuExitParams> SetMenuObservable()
        {
            var gameEntryState = new Game.GameEntryParams();
            var exitState = new MenuExitParams(gameEntryState);
            var exitSignal = _playSignal.Select(_ => exitState);
            exitSignal.Subscribe(_ =>
            {
                _menuDi.Resolve<Utils.UI>().DetachUI<MenuUI>();
                _menuDi.Resolve<Utils.UI>().Clear();
                _menuDi.Resolve<IProjectDataProvider>().SaveProjectData();
            });
            return exitSignal;
        }
    }
}