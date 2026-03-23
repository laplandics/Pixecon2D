using System.Collections.Generic;
using Cmd;
using ProjectSpace;
using R3;
using UnityEngine;
using Utils;

namespace Menu
{
    public class MenuBoot : MonoBehaviour
    {
        public Observable<MenuExitParams> Boot(Core.DI menuDi, MenuEntryParams entryParams)
        {
            menuDi.Register(_ => new Cam("MenuCam"), true);

            menuDi.Resolve<Cam>().Instantiate();
            menuDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdCreateVocabularyHandler(
                menuDi.Resolve<IProjectDataProvider>().ProjectData));
            menuDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdCreateVocabularyEntryHandler(
                menuDi.Resolve<IProjectDataProvider>().ProjectData));
            
            menuDi.Register(_ => new VocabularyCreator(
                menuDi.Resolve<ICommandProcessor>(),
                menuDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies));
            
            var playSignal = new Subject<Unit>();
            menuDi.Register(_ => new MenuUiInteractor(
                menuDi.Resolve<UI>(),
                menuDi.Resolve<VocabularyCreator>(),
                new Dictionary<string, Subject<Unit>>
                    { [MenuUiInteractor.PLAY_BUTTON_SIGNAL_NAME] = playSignal}));
            
            menuDi.Resolve<MenuUiInteractor>().Instantiate();
            
            var gameEntryState = new Game.GameEntryParams();
            var exitState = new MenuExitParams(gameEntryState);
            var exitSignal = playSignal.Select(_ => exitState);
            exitSignal.Subscribe(_ =>
            {
                menuDi.Resolve<UI>().DetachUIRootBinder<MenuUIRootBinder>();
                menuDi.Resolve<UI>().Clear();
                menuDi.Resolve<IProjectDataProvider>().SaveProjectData();
            });
            
            return exitSignal;
        }
    }
}