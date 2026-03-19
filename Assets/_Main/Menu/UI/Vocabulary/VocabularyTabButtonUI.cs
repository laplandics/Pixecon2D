using System.Collections.Generic;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class VocabularyTabButtonUI : MonoBehaviour
    {
        [Header("References")]
        public Button tabButton;
        public TMP_Text tabName;
        public Image tabBg;
        public Image tabIcon;

        [Header("Prefabs")]
        public VocabularyTitleUI vocabularyTitlePrefab;
        public VocabularyEntryUI vocabularyEntryPrefab;
        public VocabularyAddEntryUI vocabularyAddEntryPrefab;
        
        [Header("Settings")]
        public Color iconSelectedColor;
        public Color bgSelectedColor;
        [Space]
        public Color iconUnselectedColor;
        public Color bgUnselectedColor;
        
        private RectTransform _container;
        private VocabularyTitleUI _vocabularyTitleUI;
        private VocabularyAddEntryUI _vocabularyAddEntryUI;
        private readonly List<VocabularyEntryUI> _vocabularyEntryUis = new();
        
        public void SetSelected()
        {
            tabBg.color = bgSelectedColor;
            tabIcon.color = iconSelectedColor;
            tabName.color = iconSelectedColor;
        }

        public void SetUnselected()
        {
            tabBg.color = bgUnselectedColor;
            tabIcon.color = iconUnselectedColor;
            tabName.color = iconUnselectedColor;
        }

        public void ShowVocabularyInfo(RectTransform container)
        {
            _container = container;
            CreateTitle();
            CreateExistingEntriesUIs();
            CreateAddEntryUI();
        }

        private void CreateTitle()
        {
            _vocabularyTitleUI = Instantiate(vocabularyTitlePrefab, _container, false);
            _vocabularyTitleUI.titleInput.text = VocabularyProxy.Title.Value;
            _vocabularyTitleUI.titleInput.onValueChanged.AddListener(text => VocabularyProxy.Title.Value = text);
        }

        private void CreateExistingEntriesUIs()
        {
            foreach (var entryDataProxy in VocabularyProxy.VocabularyEntries)
            { InstantiateEntry(entryDataProxy); }
        }

        private void InstantiateEntry(Proxy.VocabularyEntryDataProxy entryDataProxy)
        {
            var entryUi = Instantiate(vocabularyEntryPrefab, _container, false);
            entryUi.translationInput.text = entryDataProxy.Translation.Value;
            entryUi.wordInput.text = entryDataProxy.Word.Value;

            entryUi.translationInput.onValueChanged.AddListener(text => entryDataProxy.Translation.Value = text);
            entryUi.wordInput.onValueChanged.AddListener(text => entryDataProxy.Word.Value = text);
                
            _vocabularyEntryUis.Add(entryUi);
        }

        private void CreateAddEntryUI()
        {
            _vocabularyAddEntryUI = Instantiate(vocabularyAddEntryPrefab, _container, false);
            _vocabularyAddEntryUI.addEntryButton.onClick.AddListener(OnAddEntryButtonClicked);
        }

        private void OnAddEntryButtonClicked()
        {
            var entryDataProxySignal = new ReactiveProperty<Proxy.VocabularyEntryDataProxy>();
            entryDataProxySignal.Skip(1).Subscribe(InstantiateEntry);
            
            // Command processor use
            Debug.LogWarning("Move command processor registration to menu registrations");
            var cmd = new Cmd.CommandProcessor();
            cmd.RegisterHandler(new CmdCreateVocabularyEntryHandler(VocabularyProxy));
            cmd.Process(new CmdCreateVocabularyEntry("", "", entryDataProxySignal));
            
            DestroyAddEntryUI();
            CreateAddEntryUI();
            entryDataProxySignal.OnCompleted();
        }

        private void DestroyAddEntryUI()
        {
            if (_vocabularyAddEntryUI != null)
                Destroy(_vocabularyAddEntryUI.gameObject);
        }

        public void ClearVocabularyInfo()
        {
            if (_vocabularyTitleUI != null) 
                Destroy(_vocabularyTitleUI.gameObject);
            foreach (var entryUi in _vocabularyEntryUis)
            { Destroy(entryUi.gameObject); }
            _vocabularyEntryUis.Clear();
            DestroyAddEntryUI();
        }

        public Proxy.VocabularyDataProxy VocabularyProxy { get; set; }
    }
}