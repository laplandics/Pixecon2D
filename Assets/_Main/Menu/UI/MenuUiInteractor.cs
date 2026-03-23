using System.Collections.Generic;
using ProjectSpace;
using R3;
using UnityEngine;

namespace Menu
{
    public class MenuUiInteractor
    {
        public const string PLAY_BUTTON_SIGNAL_NAME = "Play";
        
        private MenuUIRootBinder _menuUIRootBinder;
        private IMenuUiInfoPanel _currentPanel;
        private string _currentPanelName;
        private readonly UI _rootUi;
        private readonly VocabularyCreator _vocabCreator;
        private readonly Dictionary<string, Subject<Unit>> _uiSignals;

        public MenuUiInteractor(UI rootUi, VocabularyCreator vocabCreator, Dictionary<string, Subject<Unit>> uiSignals)
        {
            _rootUi = rootUi;
            _vocabCreator = vocabCreator;
            _uiSignals = uiSignals;
        }

        public MenuUIRootBinder Instantiate()
        {
            _rootUi.AttachUIRootBinder(Constant.Names.UI.MENU_UI, out _menuUIRootBinder);
            var switchInfoPanelSignalSubject = new Subject<string>();
            _menuUIRootBinder.Bind(switchInfoPanelSignalSubject);
            switchInfoPanelSignalSubject.Subscribe(OnInfoPanelSwitched);
            _menuUIRootBinder.SendDefaultSignal();
            return _menuUIRootBinder;
        }

        private void OnInfoPanelSwitched(string panelName)
        {
            if (_currentPanelName == panelName) return;
            _currentPanel?.ClearElements();
            _currentPanelName = panelName;
            var panelPrefab = Resources.Load<GameObject>(panelName);
            var newPanelObj = Object.Instantiate(panelPrefab, _menuUIRootBinder.ViewPortContainer, false);
            var newPanel = newPanelObj.GetComponent<IMenuUiInfoPanel>();
            _menuUIRootBinder.scrollRect.content = newPanelObj.GetComponent<RectTransform>();
            _currentPanel = newPanel;
            newPanel.LoadElements(_vocabCreator, _uiSignals);
        }
    }
}