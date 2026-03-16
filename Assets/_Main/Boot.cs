using System.Collections;
using DI;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot
{
    private static Boot _instance;
    
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        SetSettings();
        InitUtils();
        _instance = new Boot();
    }

    private static void SetSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private static void InitUtils()
    {
        Utils.Coroutines.Init();
        Utils.UI.Init();
    }
    
    private Boot()
    {
        
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName is Utils.Scenes.MENU or Utils.Scenes.GAME)
        { Utils.Coroutines.Start(LoadMenu(), out _); return; }
        if (sceneName != Utils.Scenes.BOOT) { return; }
#endif
        
        Utils.Coroutines.Start(LoadMenu(), out _);
    }

    private IEnumerator LoadMenu(Menu.MenuEntryState entryState = null)
    {
        Utils.UI.New(Utils.UI.LOADING_SCREEN);
        yield return Utils.Scenes.Load(Utils.Scenes.MENU);
        yield return new WaitForSeconds(1f);
        Utils.UI.Clear();
        var menu = new GameObject("Menu").AddComponent<Menu.MenuBoot>();
        menu.Boot(entryState).Subscribe(menuExitState => 
            Utils.Coroutines.Start(LoadGame(menuExitState.GameEntryState), out _));
    }

    private IEnumerator LoadGame(Game.GameEntryState entryState)
    {
        Utils.UI.New(Utils.UI.LOADING_SCREEN);
        yield return Utils.Scenes.Load(Utils.Scenes.GAME);
        yield return new WaitForSeconds(1f);
        Utils.UI.Clear();
        var game = new GameObject("Game").AddComponent<Game.GameBoot>();
        game.Boot(entryState).Subscribe(gameExitState =>
            Utils.Coroutines.Start(LoadMenu(gameExitState.MenuEntryState), out _));
    }
}