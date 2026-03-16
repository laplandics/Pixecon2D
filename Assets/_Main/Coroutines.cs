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
        private static Dictionary<Func<IEnumerator>, UnityEngine.Coroutine> _routines =  new();

        public static void Init()
        {
            if (_coroutineContainer != null) Object.Destroy(_coroutineContainer);
            _coroutineContainer = new GameObject("[COROUTINES]").AddComponent<Coroutine>();
            Object.DontDestroyOnLoad(_coroutineContainer.gameObject);
            _routines = new Dictionary<Func<IEnumerator>, UnityEngine.Coroutine>();
        }
        
        public static void Start(Func<IEnumerator> func)
        {
            Clear();
            if (func == null) return;
            var routine = func();
            var coroutine = _coroutineContainer.StartCoroutine(routine);
            _routines[func] = coroutine;
        }
        
        public static void Stop(Func<IEnumerator> func)
        {
            if (!_routines.TryGetValue(func, out var coroutine)) return;
            if (coroutine == null) {_routines.Remove(func); return; }
            _coroutineContainer.StopCoroutine(coroutine);
            _routines.Remove(func);
        }

        private static void Clear()
        {
            if (_routines.Count != 0)
            {
                var nullRoutines = new List<Func<IEnumerator>>();
                foreach (var pair in _routines)
                { if (pair.Value == null) nullRoutines.Add(pair.Key); }
                foreach (var nullRoutine in nullRoutines) { _routines.Remove(nullRoutine); }
            }
            else { _routines.Clear(); }
        }
    }
}