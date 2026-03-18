using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class VocabularyInfoPanelUI : MonoBehaviour, IMenuUiInfoPanel
    {
        [Header("References")]
        public RectTransform vocabulariesTabsButtonsContainer;

        [Header("Prefabs")]
        public VocabularyTabButtonUI vocabularyTabButtonPrefab;

        private int _buttonsIndex;
        private Proxy.ProjectDataProxy _pdp;
        private VocabularyTabButtonUI _newVocabularyTabButton;
        private readonly List<VocabularyTabButtonUI> _vocabularyTabButtons = new();
        
        public void LoadElements(Proxy.ProjectDataProxy pdp)
        {
            _pdp = pdp;
            _buttonsIndex = 0;
            LoadVocabularyTabButtons();
            CreateNewTabVocabularyTabButton();
        }

        private void CreateNewTabVocabularyTabButton()
        {
            _newVocabularyTabButton = Instantiate(vocabularyTabButtonPrefab,
                vocabulariesTabsButtonsContainer, false);
            _newVocabularyTabButton.tabIcon.gameObject.SetActive(true);
            _newVocabularyTabButton.tabButton.onClick.AddListener(OnNewVocabularyTabButtonClicked);
        }

        private void LoadVocabularyTabButtons()
        {
            foreach (var vocabulary in _pdp.Vocabularies)
            { InstantiateTabButton(vocabulary); }
        }

        private void OnExistingVocabularyTabButtonClicked(VocabularyTabButtonUI tabButton)
        {
            foreach (var tabButtonUI in _vocabularyTabButtons)
            { tabButtonUI.SetUnselected(); tabButtonUI.ClearVocabularyInfo(); }
            tabButton.SetSelected();
            tabButton.ShowVocabularyInfo(GetComponent<RectTransform>());
        }

        private void OnNewVocabularyTabButtonClicked()
        {
            var newVocabData = new Data.VocabularyData
            {
                key = $"Vocabulary{_pdp.Vocabularies.Count}",
                isDone = false,
                isIncluded = true,
                title = $"Новый словарь {_pdp.Vocabularies.Count + 1}",
                vocabularyEntries = new List<Data.VocabularyEntryData>()
            };
            var newVocabProxy = new Proxy.VocabularyDataProxy(newVocabData);
            _pdp.Vocabularies.Add(newVocabProxy);
            Destroy(_newVocabularyTabButton.gameObject);
            InstantiateTabButton(newVocabProxy);
            CreateNewTabVocabularyTabButton();
        }

        private void InstantiateTabButton(Proxy.VocabularyDataProxy vocabulary)
        {
            var newTabButton = Instantiate(vocabularyTabButtonPrefab,
                vocabulariesTabsButtonsContainer, false);
                
            _vocabularyTabButtons.Add(newTabButton);
            newTabButton.tabIcon.gameObject.SetActive(false);
            newTabButton.tabName.gameObject.SetActive(true);
            newTabButton.tabName.text = $"{_buttonsIndex + 1}";
            newTabButton.VocabularyInfo = vocabulary;
                
            newTabButton.tabButton.onClick.AddListener(() =>
                OnExistingVocabularyTabButtonClicked(newTabButton));
                
            _buttonsIndex++;
        }
        
        public void ClearElements()
        {
            
        }
    }
}
