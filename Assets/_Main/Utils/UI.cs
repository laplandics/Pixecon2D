using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Utils
{
    public class UI
    {
        public const string LOADING_SCREEN = "LoadingScreen";
        public const string MENU_UI = "MenuUI";
        public const string GAME_UI = "GameUI";
        
        private Ui _uiContainer;
        
        public UI()
        {
            _uiContainer = new GameObject("[UI]").AddComponent<Ui>();
            Object.DontDestroyOnLoad(_uiContainer.gameObject);
            _uiContainer.uiDocument = _uiContainer.gameObject.AddComponent<UIDocument>();
            var settings = Resources.Load<PanelSettings>("UI/UISettings");
            _uiContainer.InitSettings(settings);
        }

        public void New(string uiAssetName, bool enable = true)
        {
            if (_uiContainer.uiDocument.rootVisualElement != null)
            { _uiContainer.ResetUi(); }
            var uiAsset = Resources.Load<VisualTreeAsset>($"UI/{uiAssetName}");
            _uiContainer.SetUI(uiAsset);
            _uiContainer.uiDocument.enabled = enable;
        }

        public void AttachSceneUI<T>(out T ui) where T : MonoBehaviour, ISceneUI
        {
            ISceneUI sceneUi = _uiContainer.gameObject.AddComponent<T>();
            sceneUi.OnAttached();
            ui = (T)sceneUi;
        }

        public void RemoveSceneUI<T>() where T : MonoBehaviour, ISceneUI
        {
            if (!_uiContainer.gameObject.TryGetComponent(out T sceneUi)) return;
            sceneUi.OnRemoved();
            Object.Destroy(sceneUi);
        }
        
        public void Clear() { _uiContainer.ResetUi(); }
        
        public void Enable() => _uiContainer.uiDocument.enabled = true;
        public void Disable() => _uiContainer.uiDocument.enabled = false;
        
        internal class Ui : MonoBehaviour
        {
            public UIDocument uiDocument;
            
            public void InitSettings(PanelSettings settings) => uiDocument.panelSettings = settings;
            public void SetUI(VisualTreeAsset uiAsset) { uiDocument.visualTreeAsset = uiAsset; }
            public void ResetUi() { uiDocument.visualTreeAsset = null; }
        }
    }
}