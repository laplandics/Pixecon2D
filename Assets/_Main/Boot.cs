using System.Collections;
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
        { Utils.Coroutines.Start(LoadMenu); return; }
        if (sceneName != Utils.Scenes.BOOT) { return; }
#endif
        
        Utils.Coroutines.Start(LoadMenu);
    }

    private IEnumerator LoadMenu()
    {
        Utils.UI.New(Utils.UI.LOADING_SCREEN);
        yield return Utils.Scenes.Load(Utils.Scenes.MENU);
        yield return new WaitForSeconds(1f);
        Utils.UI.Clear();
        var menu = new GameObject("Menu").AddComponent<MenuBoot>();
        menu.GoToGameScene += () => Utils.Coroutines.Start(LoadGame);
        yield return menu.Boot();
    }

    private IEnumerator LoadGame()
    {
        Utils.UI.New(Utils.UI.LOADING_SCREEN);
        yield return Utils.Scenes.Load(Utils.Scenes.GAME);
        yield return new WaitForSeconds(1f);
        Utils.UI.Clear();
        var game = new GameObject("Game").AddComponent<GameBoot>();
        game.GoToMenuScene += () => Utils.Coroutines.Start(LoadMenu);
        yield return game.Boot();
    }
}