using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class GameUI : MonoBehaviour, ISceneUI
    {
        private Subject<Unit> _exitSignal;
        private UIDocument _uiDocument;
        private Button _pauseButton;
        
        public void OnAttached()
        {
            AssignReferences();
            AssignButtons();
        }

        private void AssignReferences()
        {
            _uiDocument = GetComponent<UIDocument>();
            _pauseButton = _uiDocument.rootVisualElement.Q<Button>("PauseButton");
        }

        private void AssignButtons()
        {
            _pauseButton.clicked += OnPauseButtonClicked;
        }

        private void OnPauseButtonClicked()
        {
            _exitSignal?.OnNext(Unit.Default);
        }

        public void OnRemoved()
        {
            _pauseButton.clicked -= OnPauseButtonClicked;
        }

        public void Bind(Subject<Unit> exitSignal)
        {
            _exitSignal = exitSignal;
        }
    }
}
