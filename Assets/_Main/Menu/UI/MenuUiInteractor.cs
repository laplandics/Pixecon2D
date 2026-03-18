using R3;
using UnityEngine;

namespace Menu
{
    public class MenuUiInteractor
    {
        private readonly Utils.UI _rootUi;
        private readonly Proxy.ProjectDataProxy _projectDataProxy;
        private string _currentPanelName;
        private MenuUI _menuUI;
        
        public MenuUiInteractor(Utils.UI rootUi, Proxy.ProjectDataProxy projectDataProxy)
        { _rootUi = rootUi; _projectDataProxy = projectDataProxy; }

        public MenuUI Instantiate()
        {
            _rootUi.AttachUI(Constant.Names.UI.MENU_UI, out _menuUI);
            var switchInfoPanelSignalSubject = new Subject<string>();
            _menuUI.Bind(switchInfoPanelSignalSubject);
            switchInfoPanelSignalSubject.Subscribe(OnInfoPanelSwitched);
            return _menuUI;
        }

        private void OnInfoPanelSwitched(string panelName)
        {
            if (_currentPanelName == panelName) return;
            _currentPanelName = panelName;
            var panelPrefab = Resources.Load<GameObject>(panelName);
            var newPanelObj = Object.Instantiate(panelPrefab, _menuUI.ViewPortContainer, false);
            var newPanel = newPanelObj.GetComponent<IMenuUiInfoPanel>();
            _menuUI.scrollRect.content = newPanelObj.GetComponent<RectTransform>();
            newPanel.LoadElements(_projectDataProxy);
        }
    }
}