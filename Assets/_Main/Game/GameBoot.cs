using Cmd;
using Core;
using GameView;
using Menu;
using ProjectSpace;
using R3;
using Settings;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class GameBoot : MonoBehaviour
    {
        public void Boot(DI gameDi, GameEntryParams entryParams,
            out Observable<GameExitParams> exitObservable,
            out Observable<GameEntryParams> replayObservable)
        {
            var exitSignalSubject = new Subject<Unit>();
            var menuEntryParams = new MenuEntryParams("");
            var gameExitParams = new GameExitParams(menuEntryParams);
            exitObservable = exitSignalSubject.Select(_ => gameExitParams);
            
            var replaySignalSubject = new Subject<Unit>();
            var gameEntryParams = new GameEntryParams();
            replayObservable = replaySignalSubject.Select(_ => gameEntryParams);
            
            exitObservable.Subscribe(_ =>
            {
                gameDi.Resolve<World>().DetachWorldRootBinder<GameWorldRootBinder>();
                gameDi.Resolve<UI>().DetachUIRootBinder<GameUIRootBinder>();
                gameDi.Resolve<UI>().Clear();
            });
            
            //Registrations
            
            gameDi.Register(_ => new World(), true);
            gameDi.Register(_ => new Cam("GameCam"), true);
            
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdCreateCellHandler(
                gameDi.Resolve<IProjectDataProvider>().ProjectData));
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdDestroyCellHandler(
                gameDi.Resolve<IProjectDataProvider>().ProjectData));
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdChangeCurrentEntryHandler());
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdChangeCurrentVocabularyHandler(
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies));
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdSetCellLetterHandler(
                gameDi.Resolve<ISettingsProvider>().MenuSettings.vocabulariesSettings));
            
            gameDi.Register(_ => new CurrentVocabularyHandler(
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies,
                gameDi.Resolve<ICommandProcessor>()), true);
            gameDi.Register(_ => new GameInputHandler(new Inputs()), true);
            gameDi.Register(_ => new CellBuilder(
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Cells,
                gameDi.Resolve<ICommandProcessor>()), true);
            gameDi.Register(_ => new CellLetterSetter(
                gameDi.Resolve<CurrentVocabularyHandler>(),
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Cells,
                gameDi.Resolve<ICommandProcessor>()), true);
            gameDi.Register(_ => new GameGrid(gameDi.Resolve<Cam>()), true);
            gameDi.Register(_ => new ChosenLetterChecker(
                gameDi.Resolve<CurrentVocabularyHandler>()), true);
            gameDi.Register(_ => new GameCycleHandler(
                exitSignalSubject,
                replaySignalSubject), true);
            gameDi.Register(_ => new GamePopupHandler(
                gameDi.Resolve<GameCycleHandler>(), 
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies), true);
            gameDi.Register(_ => new CellClickHandler(
                gameDi.Resolve<GameInputHandler>(),
                gameDi.Resolve<Cam>(),
                gameDi.Resolve<UI>().GetCanvas.GetComponent<GraphicRaycaster>(),
                gameDi.Resolve<UI>().GetEventSystem,
                gameDi.Resolve<ChosenLetterChecker>(),
                gameDi.Resolve<CellBuilder>()), true);
            gameDi.Register(_ => new GameField(
                gameDi.Resolve<GameGrid>(),
                gameDi.Resolve<CellBuilder>(),
                gameDi.Resolve<CellLetterSetter>(),
                gameDi.Resolve<CellClickHandler>(),
                gameDi.Resolve<CurrentVocabularyHandler>()), true);
            
            gameDi.Register(_ => new GameWorldRootViewModel(gameDi.Resolve<CellBuilder>()), true);
            gameDi.Register(_ => new GameUIRootViewModel(
                gameDi.Resolve<GameCycleHandler>(),
                gameDi.Resolve<GamePopupHandler>(),
                gameDi.Resolve<CurrentVocabularyHandler>()), true);
            
            
            //Essentials
            
            gameDi.Resolve<Cam>().Instantiate();
            gameDi.Resolve<UI>().AttachUIRootBinder(
                Constant.Names.UI.GAME_UI,
                out GameUIRootBinder uiRootBinder);
            gameDi.Resolve<GameField>().CreateField();
            gameDi.Resolve<World>().AttachWorldRootBinder(Constant.Names.World.GAME_WORLD,
                out GameWorldRootBinder worldRootBinder);
            
            
            //Binding (World)
            
            worldRootBinder.Bind(gameDi.Resolve<GameWorldRootViewModel>());
            
            
            //Binding (UI)
            
            uiRootBinder.Bind(gameDi.Resolve<GameUIRootViewModel>());
        }
    }
}