using System.Collections;
using Cmd;
using Core;
using Data;
using R3;
using ProjectSpace;
using Settings;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Boot
{
    private static Boot _instance;
    private readonly DI _rootDi;
    private DI _sceneDi;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap() { _instance = new Boot(); }
    
    private Boot()
    {
        _rootDi = new DI();
        _rootDi.Register(_ => new Coroutines(), true);
        _rootDi.Register(_ => new UI(), true);
        _rootDi.Register<ICommandProcessor>(_ => new CommandProcessor(), true);
        _rootDi.Register<ISettingsProvider>(_ => new LocalSettingsProvider(), true);
        _rootDi.Register<IProjectDataProvider>(_ => new PlayerPrefsProjectDataProvider(), true);

        var appSettings = _rootDi.Resolve<ISettingsProvider>().ApplicationSettings;
        QualitySettings.vSyncCount = appSettings.vsyncCount;
        Application.targetFrameRate = appSettings.fpsLock;
        Screen.sleepTimeout = appSettings.neverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        
#if UNITY_EDITOR
        Debug.LogWarning("Remove temporal editor code (Boot scene)");
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName is Constant.Names.Scenes.MENU or Constant.Names.Scenes.GAME)
        { _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _); return; }
        if (sceneName != Constant.Names.Scenes.BOOT) { return; }
#endif
        
        _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _);
    }

    private IEnumerator LoadMenu(Menu.MenuEntryParams entryParams = null)
    {
        yield return new WaitForTask(_rootDi.Resolve<ISettingsProvider>().LoadSettingsAsync());
        
        var projectDataLoaded = false;
        _rootDi.Resolve<IProjectDataProvider>().LoadProjectData(_rootDi.Resolve<ISettingsProvider>())
            .Subscribe(_ => projectDataLoaded = true);
        yield return new WaitUntil(() => projectDataLoaded);
        
        _rootDi.Dispose();
        _sceneDi?.Dispose();
        _sceneDi = new DI(_rootDi);
        _rootDi.Resolve<UI>().AttachUIRootBinder<LoadingScreen>(Constant.Names.UI.LOADING_SCREEN, out _);
        yield return Scenes.Load(Constant.Names.Scenes.MENU);
        yield return new WaitForSeconds(1f);
        _rootDi.Resolve<UI>().Clear();
        
        var menu = new GameObject("Menu").AddComponent<Menu.MenuBoot>();
        menu.Boot(_sceneDi, entryParams).Subscribe(menuExitParams => 
            _rootDi.Resolve<Coroutines>().Start(LoadGame(menuExitParams.GameEntryParams), out _));
    }

    private IEnumerator LoadGame(Game.GameEntryParams entryParams)
    {
        yield return new WaitForTask(_rootDi.Resolve<ISettingsProvider>().LoadSettingsAsync());
        
        var projectDataLoaded = false;
        _rootDi.Resolve<IProjectDataProvider>().LoadProjectData(_rootDi.Resolve<ISettingsProvider>())
            .Subscribe(_ => projectDataLoaded = true);
        yield return new WaitUntil(() => projectDataLoaded);
        
        _rootDi.Dispose();
        _sceneDi?.Dispose();
        _sceneDi = new DI(_rootDi);
        _rootDi.Resolve<UI>().AttachUIRootBinder<LoadingScreen>(Constant.Names.UI.LOADING_SCREEN, out _);
        yield return Scenes.Load(Constant.Names.Scenes.GAME);
        yield return new WaitForSeconds(1f);
        _rootDi.Resolve<UI>().Clear();
        
        var game = new GameObject("Game").AddComponent<Game.GameBoot>();
        game.Boot(_sceneDi, entryParams).Subscribe(gameExitParams =>
            _rootDi.Resolve<Coroutines>().Start(LoadMenu(gameExitParams.MenuEntryParams), out _));
    }
}