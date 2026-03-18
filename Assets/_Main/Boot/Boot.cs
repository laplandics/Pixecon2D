using System.Collections;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot
{
    private static Boot _instance;
    private readonly Core.DI _rootDi;
    private Core.DI _sceneDi;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap() { SetSettings(); _instance = new Boot(); }
    
    private static void SetSettings()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    private Boot()
    {
        _rootDi = new Core.DI();
        _rootDi.Register(_ => new Utils.Coroutines(), true);
        _rootDi.Register(_ => new Utils.UI(), true);
        _rootDi.Register<IProjectDataProvider>(_ => new Core.PlayerPrefsProjectDataProvider(), true);
        
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName is Constant.Names.Scenes.MENU or Constant.Names.Scenes.GAME)
        { _rootDi.Resolve<Utils.Coroutines>().Start(LoadMenu(), out _); return; }
        if (sceneName != Constant.Names.Scenes.BOOT) { return; }
#endif
        
        _rootDi.Resolve<Utils.Coroutines>().Start(LoadMenu(), out _);
    }

    private IEnumerator LoadMenu(Menu.MenuEntryParams entryParams = null)
    {
        var projectDataLoaded = false;
        _rootDi.Resolve<IProjectDataProvider>().LoadProjectData().Subscribe(_ => projectDataLoaded = true);
        yield return new WaitUntil(() => projectDataLoaded);
        
        _sceneDi?.Dispose();
        _sceneDi = new Core.DI(_rootDi);
        _rootDi.Resolve<Utils.UI>().AttachUI<LoadingScreen>(Constant.Names.UI.LOADING_SCREEN, out _);
        yield return Utils.Scenes.Load(Constant.Names.Scenes.MENU);
        yield return new WaitForSeconds(1f);
        _rootDi.Resolve<Utils.UI>().Clear();
        
        var menu = new GameObject("Menu").AddComponent<Menu.MenuBoot>();
        menu.Boot(_sceneDi, entryParams).Subscribe(menuExitParams => 
            _rootDi.Resolve<Utils.Coroutines>().Start(LoadGame(menuExitParams.GameEntryParams), out _));
    }

    private IEnumerator LoadGame(Game.GameEntryParams entryParams)
    {
        var projectDataLoaded = false;
        _rootDi.Resolve<IProjectDataProvider>().LoadProjectData().Subscribe(_ => projectDataLoaded = true);
        yield return new WaitUntil(() => projectDataLoaded);
        
        _sceneDi?.Dispose();
        _sceneDi = new Core.DI(_rootDi);
        _rootDi.Resolve<Utils.UI>().AttachUI<LoadingScreen>(Constant.Names.UI.LOADING_SCREEN, out _);
        yield return Utils.Scenes.Load(Constant.Names.Scenes.GAME);
        yield return new WaitForSeconds(1f);
        _rootDi.Resolve<Utils.UI>().Clear();
        
        var game = new GameObject("Game").AddComponent<Game.GameBoot>();
        game.Boot(_sceneDi, entryParams).Subscribe(gameExitParams =>
            _rootDi.Resolve<Utils.Coroutines>().Start(LoadMenu(gameExitParams.MenuEntryParams), out _));
    }
}