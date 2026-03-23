using Cmd;
using Core;
using GameView;
using ProjectSpace;
using R3;
using Settings;
using UnityEngine;
using Utils;

namespace Game
{
    public class GameBoot : MonoBehaviour
    {
        public Observable<GameExitParams> Boot(DI gameDi, GameEntryParams entryParams)
        {
            var exitSignalSubject = new Subject<Unit>();
            var menuEntryParams = new Menu.MenuEntryParams("");
            var gameExitParams = new GameExitParams(menuEntryParams);
            var exitObservable = exitSignalSubject.Select(_ => gameExitParams);
            
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
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdSetCellLetterHandler(
                gameDi.Resolve<ISettingsProvider>().MenuSettings.vocabulariesSettings));
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdExitGameHandler());
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdChangeCurrentTranslationHandler(
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies));
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdDestroyCellHandler(
                gameDi.Resolve<IProjectDataProvider>().ProjectData));
            
            gameDi.Register(_ => new GameInputHandler(new Inputs()), true);
            gameDi.Register(_ => new CellClickHandler(
                gameDi.Resolve<GameInputHandler>(),
                gameDi.Resolve<Cam>(),
                gameDi.Resolve<ICommandProcessor>()), true);
            gameDi.Register(_ => new CellBuilder(
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Cells,
                gameDi.Resolve<ICommandProcessor>()), true);
            gameDi.Register(_ => new CellLetterSetter(
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Cells,
                gameDi.Resolve<ICommandProcessor>()), true);
            
            gameDi.Resolve<ICommandProcessor>().RegisterHandler(new CmdHandleCellClickHandler(
                gameDi.Resolve<CellBuilder>()));
            
            gameDi.Register(_ => new GameGrid(gameDi.Resolve<Cam>()), true);
            gameDi.Register(_ => new TranslationChanger(
                gameDi.Resolve<ICommandProcessor>(),
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies), true);
            gameDi.Register(_ => new ChosenLetterHandler(
                gameDi.Resolve<CellBuilder>(),
                gameDi.Resolve<IProjectDataProvider>().ProjectData.Vocabularies), true);
            gameDi.Register(_ => new GameFinisher(
                gameDi.Resolve<ICommandProcessor>(),
                exitSignalSubject), true);
            gameDi.Register(_ => new GameField(
                gameDi.Resolve<GameGrid>(),
                gameDi.Resolve<CellBuilder>(),
                gameDi.Resolve<CellLetterSetter>(),
                gameDi.Resolve<CellClickHandler>(),
                gameDi.Resolve<ChosenLetterHandler>()), true);
            
            gameDi.Register(_ => new GameWorldRootViewModel(gameDi.Resolve<CellBuilder>()), true);
            gameDi.Register(_ => new GameUIRootViewModel(
                gameDi.Resolve<GameFinisher>(),
                gameDi.Resolve<TranslationChanger>()), true);
            
            
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
            
            return exitObservable;
        }
    }
}