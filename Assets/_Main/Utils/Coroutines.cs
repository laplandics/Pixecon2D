using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class Coroutines
    {
        internal class Coroutine : MonoBehaviour { }
        private readonly Coroutine _coroutineContainer;

        public Coroutines()
        {
            _coroutineContainer = new GameObject("[COROUTINES]").AddComponent<Coroutine>();
            Object.DontDestroyOnLoad(_coroutineContainer.gameObject);
        }
        
        public void Start(IEnumerator enumerator, out UnityEngine.Coroutine coroutine,
            MonoBehaviour coroutineHolder = null)
        {
            if (coroutineHolder == null) coroutineHolder = _coroutineContainer;
            coroutine = null; if (enumerator == null) return;
            coroutine = coroutineHolder.StartCoroutine(enumerator);
        }

        public void Stop(UnityEngine.Coroutine coroutine, MonoBehaviour coroutineHolder = null)
        {
            if (coroutine == null) return;
            if (coroutineHolder == null) coroutineHolder = _coroutineContainer;
            coroutineHolder.StopCoroutine(coroutine);
        }
    }
}