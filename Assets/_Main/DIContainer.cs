
using System;
using System.Collections.Generic;

namespace DI
{
    public class DIContainer
    {
        private readonly DIContainer _parentContainer;
        private readonly Dictionary<(string, Type), DIRegistration> _registrations = new();
        private readonly HashSet<(string, Type)> _resolutions = new();
        
        public DIContainer(DIContainer parent = null) { _parentContainer = parent; }

        public void RegisterSingleton<T>(Func<DIContainer, T> factory) { RegisterSingleton(null, factory); }

        public void RegisterSingleton<T>(string tag, Func<DIContainer, T> factory)
        { var key = (tag, typeof(T)); Register(key, factory, true); }
        
        public void RegisterTransient<T>(Func<DIContainer, T> factory) { RegisterTransient(null, factory); }
        
        public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
        { var key = (tag, typeof(T)); Register(key, factory, false); }

        public void RegisterInstance<T>(T instance) { RegisterInstance(null, instance); }
        
        public void RegisterInstance<T>(string tag, T instance)
        {
            var key = (tag, typeof(T));
            if (_registrations.ContainsKey(key))
            { throw new Exception($"Duplicate registration for {key.tag} of type {key.Item2.FullName}"); }
            _registrations[key] = new DIRegistration
            { Instance = instance, IsSingleton = false };
        }
        
        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
        {
            if (_registrations.ContainsKey(key))
            { throw new Exception($"Duplicate registration for {key.Item1} of type {key.Item2.FullName}"); }

            _registrations[key] = new DIRegistration
            { Factory = c => factory(c), IsSingleton = isSingleton };
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));
            if (!_resolutions.Add(key))
            {throw new Exception($"Duplicate registration for {key.tag} of type {key.Item2.FullName}"); }

            try
            {
                if (_registrations.TryGetValue(key, out var reg))
                {
                    if (!reg.IsSingleton) return (T)reg.Factory(this);
                
                    if (reg.Instance == null && reg.Factory != null) { reg.Instance = reg.Factory(this); }
                    return (T)reg.Instance;
                }
            
                if (_parentContainer != null) return _parentContainer.Resolve<T>(tag);
            }
            finally { _resolutions.Remove(key); }
            
            throw new Exception($"Did not find object with tag {tag} of type {key.Item2.FullName}");
        }
    }
}
