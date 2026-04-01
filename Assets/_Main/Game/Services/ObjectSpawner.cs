using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectSpawner
    {
        private readonly Dictionary<string, List<GameObject>> _objectsPool = new();

        public GameObject Spawn(string path, Vector2 position)
        {
            if (!_objectsPool.TryGetValue(path, out var pooledObjects))
            { return CreateObject(path, position, true); }
            
            foreach (var pooledObject in pooledObjects)
            {
                if (pooledObject.activeSelf) continue;
                pooledObject.transform.position = position;
                pooledObject.SetActive(true);
                return pooledObject;
            }
            
            return CreateObject(path, position, false);
        }

        private GameObject CreateObject(string path, Vector2 position, bool createNewPool)
        {
            var prefab = Resources.Load<GameObject>(path);
            var obj = Object.Instantiate(prefab, position, Quaternion.identity);
            
            if (createNewPool) _objectsPool[path] = new List<GameObject> { obj };
            else _objectsPool[path].Add(obj);
            
            return obj;
        }
    }
}