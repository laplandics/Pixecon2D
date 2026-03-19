using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Menu
{
    public class MenuUiInteractor
    {
        public const string PLAY_BUTTON_SIGNAL_NAME = "Play";
        
        private MenuUI _menuUI;
        private IMenuUiInfoPanel _currentPanel;
        private string _currentPanelName;
        private readonly Utils.UI _rootUi;
        private readonly VocabularyCreator _vocabCreator;
        private readonly Dictionary<string, Subject<Unit>> _uiSignals;

        public MenuUiInteractor(Utils.UI rootUi, VocabularyCreator vocabCreator, Dictionary<string, Subject<Unit>> uiSignals)
        {
            _rootUi = rootUi;
            _vocabCreator = vocabCreator;
            _uiSignals = uiSignals;
        }

        public MenuUI Instantiate()
        {
            _rootUi.AttachUI(Constant.Names.UI.MENU_UI, out _menuUI);
            var switchInfoPanelSignalSubject = new Subject<string>();
            _menuUI.Bind(switchInfoPanelSignalSubject);
            switchInfoPanelSignalSubject.Subscribe(OnInfoPanelSwitched);
            _menuUI.SendDefaultSignal();
            return _menuUI;
        }

        private void OnInfoPanelSwitched(string panelName)
        {
            if (_currentPanelName == panelName) return;
            _currentPanel?.ClearElements();
            _currentPanelName = panelName;
            var panelPrefab = Resources.Load<GameObject>(panelName);
            var newPanelObj = Object.Instantiate(panelPrefab, _menuUI.ViewPortContainer, false);
            var newPanel = newPanelObj.GetComponent<IMenuUiInfoPanel>();
            _menuUI.scrollRect.content = newPanelObj.GetComponent<RectTransform>();
            _currentPanel = newPanel;
            newPanel.LoadElements(_vocabCreator, _uiSignals);
        }
    }
}