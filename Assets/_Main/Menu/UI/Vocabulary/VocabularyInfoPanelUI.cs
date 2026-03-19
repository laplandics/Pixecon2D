using System.Collections.Generic;
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
        private Proxy.ProjectDataProxy _pdp;
        private VocabularyTabButtonUI _newVocabularyTabButton;
        private readonly List<VocabularyTabButtonUI> _vocabularyTabButtons = new();
        
        public void LoadElements(Proxy.ProjectDataProxy pdp, Dictionary<string, Subject<Unit>> signals)
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
            var newVocabProxySignal = new ReactiveProperty<Proxy.VocabularyDataProxy>();
            newVocabProxySignal.Skip(1).Subscribe(InstantiateTabButton);
            
            //Command processor use
            Debug.LogWarning("Move command processor registration to menu registrations");
            var cmd = new Cmd.CommandProcessor();
            cmd.RegisterHandler(new CmdCreateVocabularyHandler(_pdp));
            cmd.Process(new CmdCreateVocabulary($"Новый словарь {_pdp.Vocabularies.Count + 1}", newVocabProxySignal));
            
            Destroy(_newVocabularyTabButton.gameObject);
            CreateNewTabVocabularyTabButton();
            newVocabProxySignal.OnCompleted();
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
