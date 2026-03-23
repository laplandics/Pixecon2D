using UnityEngine;

namespace ProjectSpace
{
    public class World
    {
        private readonly WorldContainer _worldContainer;

        public World()
        {
            _worldContainer = new GameObject("").AddComponent<WorldContainer>();
            _worldContainer.name = "[WORLD]";
        }
        
        public void AttachWorldRootBinder<T>(string path, out T attachedWorld,
            bool enable = true) where T : IRootWorld
        {
            var newWorldPrefab = Resources.Load<GameObject>(path);
            var worldObj = Object.Instantiate(newWorldPrefab);
            var world = worldObj.GetComponent<IRootWorld>();
            var worldTransform = world.WorldTransform;
            worldObj.name = $"{typeof(T).Name}";
            worldObj.gameObject.SetActive(enable);
            worldTransform.SetParent(_worldContainer.transform, false);
            world.OnAttached();
            Resources.UnloadUnusedAssets();
            attachedWorld = (T)world;
        }

        public void DetachWorldRootBinder<T>() where T : IRootWorld
        {
            foreach (var child in _worldContainer.GetComponentsInChildren<T>())
            {
                var childObj = child.WorldTransform.gameObject;
                if (childObj.name != $"{typeof(T).Name}") continue;
                child.OnRemoved();
                Object.Destroy(child.WorldTransform.gameObject);
                break;
            }
        }
        
        public void Clear() { _worldContainer.ResetWorld(); }
    }
}

public class WorldContainer : MonoBehaviour
{
    public void ResetWorld()
    {
        for (var child = 0; child < transform.childCount; child++)
        { Destroy(transform.GetChild(child).gameObject); }
    }
}