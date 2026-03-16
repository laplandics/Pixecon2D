using System.Collections;
using UnityEngine.SceneManagement;

namespace Utils
{
    public static class Scenes
    {
        public const string BOOT = "Boot";
        public const string MENU = "Menu";
        public const string GAME = "Game";
        
        public static IEnumerator Load(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(BOOT);
            yield return null;
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }
    }
}