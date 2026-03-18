using System.Collections;
using UnityEngine.SceneManagement;

namespace Utils
{
    public static class Scenes
    {
        public static IEnumerator Load(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(Constant.Names.Scenes.BOOT);
            yield return null;
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }
    }
}