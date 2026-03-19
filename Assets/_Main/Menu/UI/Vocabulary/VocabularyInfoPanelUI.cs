using System.Collections.Generic;
using ObservableCollections;
using R3;
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
        private VocabularyCreator _vocabularyCreator;
        private VocabularyTabButtonUI _newVocabularyTabButton;
        private readonly List<VocabularyTabButtonUI> _vocabularyTabButtons = new();
        
        public void LoadElements(VocabularyCreator vocabCreator, Dictionary<string, Subject<Unit>> signals)
        {
            _vocabularyCreator = vocabCreator;
            _buttonsIndex = 0;
            _vocabularyCreator.GetVocabularies.ObserveAdd().Subscribe(e =>
                InstantiateTabButton(e.Value));
            
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
            foreach (var vocabulary in _vocabularyCreator.GetVocabularies)
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
            _vocabularyCreator.CreateVocabulary($"Новый словарь {_vocabularyCreator.GetVocabularies.Count + 1}");
            Destroy(_newVocabularyTabButton.gameObject);
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
            newTabButton.VocabularyProxy = vocabulary;
            newTabButton.Initialize(_vocabularyCreator);
            newTabButton.tabButton.onClick.AddListener(() =>
                OnExistingVocabularyTabButtonClicked(newTabButton));
                
            _buttonsIndex++;
        }
        
        public void ClearElements()
        {
            Destroy(gameObject);
        }
    }
}
