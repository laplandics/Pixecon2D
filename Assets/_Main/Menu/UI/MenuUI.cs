using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Menu
{
    public class MenuUI : MonoBehaviour, ISceneUI
    {
        [Header("Bottom UI")]
        public Button playButton;
        public Button vocabularyButton;
        public Button preferencesButton;
        public Button aboutButton;
        
        [Space]
        [Header("Main UI")]
        public TMP_Text infoPanelTitle;
        public ScrollRect scrollRect;
        public RectTransform viewPortContainer;
        
        private Subject<Unit> _playSignal;
        private Subject<string> _newPanelSignal;

        public void OnAttached()
        {
            AssignButtons(); 
        }

        private void AssignButtons()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            vocabularyButton.onClick.AddListener(OnVocabularyButtonClicked);
            preferencesButton.onClick.AddListener(OnPreferencesButtonClicked);
            aboutButton.onClick.AddListener(OnAboutButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            infoPanelTitle.text = "ИГРАТЬ";
            _playSignal.OnNext(Unit.Default);
        }

        private void OnVocabularyButtonClicked()
        {
            infoPanelTitle.text = "СЛОВАРИ";
            _newPanelSignal.OnNext(Constant.Names.UI.VOCABULARY_INFO_PANEL);
        }

        private void OnPreferencesButtonClicked()
        {
            infoPanelTitle.text = "НАСТРОЙКИ";
        }

        private void OnAboutButtonClicked()
        {
            infoPanelTitle.text = "О НАС";
        }

        public void OnRemoved()
        {
            
        }
        
        public void Bind(Subject<Unit> playSignal) { _playSignal = playSignal; }
        public void Bind(Subject<string> panelSignal) { _newPanelSignal = panelSignal; }
        
        public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
        public RectTransform ViewPortContainer => viewPortContainer;
    }
}
