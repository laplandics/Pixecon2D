using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Menu
{
    public class MenuUI : MonoBehaviour, ISceneUI
    {
        private Subject<Unit> _playSignal;   
        private UIDocument _uiDocument;
        private Button _playButton;
        private Button _vocabularyButton;
        private Button _preferencesButton;
        private Button _exitButton;
        
        public void OnAttached()
        {
            AssignReferences();
            AssignButtons();
        }

        private void AssignReferences()
        {
            _uiDocument = GetComponent<UIDocument>();
            _playButton = _uiDocument.rootVisualElement.Q<Button>("PlayButton");
            _vocabularyButton = _uiDocument.rootVisualElement.Q<Button>("VocabularyButton");
            _preferencesButton = _uiDocument.rootVisualElement.Q<Button>("PreferencesButton");
            _exitButton = _uiDocument.rootVisualElement.Q<Button>("ExitButton");
        }

        private void AssignButtons()
        {
            _playButton.clicked += OnPlayButtonClicked;
            _vocabularyButton.clicked += OnVocabularyButtonClicked;
            _preferencesButton.clicked += OnPreferencesButtonClicked;
            _exitButton.clicked += OnExitButtonClicked;
        }

        private void OnPlayButtonClicked()
        {
            _playSignal.OnNext(Unit.Default);
        }

        private void OnVocabularyButtonClicked()
        {
            
        }

        private void OnPreferencesButtonClicked()
        {
            
        }

        private void OnExitButtonClicked()
        {
            
        }

        public void OnRemoved()
        {
            _playButton.clicked -= OnPlayButtonClicked;
            _vocabularyButton.clicked -= OnVocabularyButtonClicked;
            _preferencesButton.clicked -= OnPreferencesButtonClicked;
            _exitButton.clicked -= OnExitButtonClicked;
        }
        
        public void Bind(Subject<Unit> playSignal)
        { _playSignal = playSignal; }
    }
}
