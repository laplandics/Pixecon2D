using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class UI
    {
        private readonly Ui _uiContainer;
        
        public UI()
        {
            var containerPrefab = Resources.Load<Ui>(Constant.Names.UI.UI_ROOT);
            _uiContainer = Object.Instantiate(containerPrefab);
            _uiContainer.name = "[UI]";
            Object.DontDestroyOnLoad(_uiContainer);
            Resources.UnloadUnusedAssets();
        }

        public void AttachUI<T>(string path, out T attachedUI,
            bool enable = true, int layer = 0, string key = "") where T : ISceneUI
        {
            var newUIPrefab = Resources.Load<GameObject>(path);
            var uiObj = Object.Instantiate(newUIPrefab);
            var ui = uiObj.GetComponent<ISceneUI>();
            var uiRect = ui.UITransform;
            uiObj.name = $"{typeof(T).Name}{key}";
            uiObj.gameObject.SetActive(enable);
            uiRect.SetParent(_uiContainer.layers[layer], false);
            ui.OnAttached();
            Resources.UnloadUnusedAssets();
            attachedUI = (T)ui;
        }

        public void DetachUI<T>(string key = "") where T : ISceneUI
        {
            foreach (var child in _uiContainer.GetComponentsInChildren<T>())
            {
                var childObj = child.UITransform.gameObject;
                if (childObj.name != $"{typeof(T).Name}{key}") continue;
                child.OnRemoved();
                Object.Destroy(child.UITransform.gameObject);
                break;
            }
        }
        
        public void Clear() { _uiContainer.ResetUi(); }
    }
}