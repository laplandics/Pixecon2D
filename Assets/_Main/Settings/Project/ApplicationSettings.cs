using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Settings/Application")]
    public class ApplicationSettings : ScriptableObject
    {
        public int vsyncCount;
        public int fpsLock;
        public int volume;
        public bool neverSleep;
    }
}