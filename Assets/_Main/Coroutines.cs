using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class Coroutines
    {
        internal class Coroutine : MonoBehaviour { }
        private static Coroutine _coroutineContainer;

        public static void Init()
        {
            if (_coroutineContainer != null) Object.Destroy(_coroutineContainer);
            _coroutineContainer = new GameObject("[COROUTINES]").AddComponent<Coroutine>();
            Object.DontDestroyOnLoad(_coroutineContainer.gameObject);
        }
        
        public static void Start(IEnumerator enumerator, out UnityEngine.Coroutine coroutine)
        {
            coroutine = null; if (enumerator == null) return;
            coroutine = _coroutineContainer.StartCoroutine(enumerator);
        }
        
        public static void Stop(UnityEngine.Coroutine coroutine)
        { if (coroutine == null) return; _coroutineContainer.StopCoroutine(coroutine); }
    }
}