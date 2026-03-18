using System.Collections.Generic;
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
            _vocabularyTitleUI.titleInput.text = VocabularyInfo.Title.Value;
            _vocabularyTitleUI.titleInput.onValueChanged.AddListener(text => VocabularyInfo.Title.Value = text);
        }

        private void CreateExistingEntriesUIs()
        {
            foreach (var entryDataProxy in VocabularyInfo.VocabularyEntries)
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
            var newEntryData = new Data.VocabularyEntryData
            {
                key = $"Entry{VocabularyInfo.VocabularyEntries.Count}",
                isDone = false,
                translation = "",
                word = ""
            };
            
            var newEntryDataProxy = new Proxy.VocabularyEntryDataProxy(newEntryData);
            VocabularyInfo.VocabularyEntries.Add(newEntryDataProxy);
            
            DestroyAddEntryUI();
            InstantiateEntry(newEntryDataProxy);
            CreateAddEntryUI();
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

        public Proxy.VocabularyDataProxy VocabularyInfo { get; set; }
    }
}