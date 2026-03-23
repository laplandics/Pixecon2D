using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectSpace
{
    public class UI
    {
        private readonly UIContainer _uiContainer;
        
        public UI()
        {
            var containerPrefab = Resources.Load<UIContainer>(Constant.Names.UI.UI_ROOT);
            _uiContainer = Object.Instantiate(containerPrefab);
            _uiContainer.name = "[UI]";
            Object.DontDestroyOnLoad(_uiContainer);
            Resources.UnloadUnusedAssets();
        }

        public void AttachUIRootBinder<T>(string path, out T attachedUI,
            bool enable = true, int layer = 0, string key = "") where T : IRootUI
        {
            var newUIPrefab = Resources.Load<GameObject>(path);
            var uiObj = Object.Instantiate(newUIPrefab);
            var ui = uiObj.GetComponent<IRootUI>();
            var uiRect = ui.UITransform;
            uiObj.name = $"{typeof(T).Name}{key}";
            uiObj.gameObject.SetActive(enable);
            uiRect.SetParent(_uiContainer.layers[layer], false);
            ui.OnAttached();
            Resources.UnloadUnusedAssets();
            attachedUI = (T)ui;
        }

        public void DetachUIRootBinder<T>(string key = "") where T : IRootUI
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