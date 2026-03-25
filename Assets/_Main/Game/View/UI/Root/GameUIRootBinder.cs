using System.Collections.Generic;
using ObservableCollections;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class GameUIRootBinder : MonoBehaviour, IRootUI
    {
        [Header("References")]
        public Button pauseButton;
        public RectTransform wordLettersContainer;
        public TMP_Text currentTranslationText;
        
        [Header("Prefabs")]
        public GameObject wordLetterPrefab;
        
        private readonly Dictionary<string, IPopup> _createdPopupsMap = new();
        private UIContainer _uiContainer;
        private readonly CompositeDisposable _disposables = new();
        private GameUIRootViewModel _vm;
        
        public void Bind(GameUIRootViewModel vm)
        {
            _vm = vm;
            
            _disposables.Add(_vm.VocabularyHandler.CurrentVocabularyEntry.Subscribe(newEntry =>
                {ChangeTranslation(newEntry.Translation.Value);}));
            _disposables.Add(_vm.VocabularyHandler.LastCorrectLetter.Skip(1).Subscribe(AddWordLetter));
            _disposables.Add(_vm.AllPopups.ObserveAdd().Subscribe(addEvent => ShowPopup(addEvent.Value)));
            _disposables.Add(_vm.AllPopups.ObserveRemove().Subscribe(removeEvent => HidePopup(removeEvent.Value)));
        }

        public void OnAttached(UIContainer container)
        {
            _uiContainer = container;
            AssignButtons();
        }

        private void AssignButtons()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }
        
        private void OnPauseButtonClicked()
        {
            _vm.CycleHandler.FinishGame();
        }

        private void ChangeTranslation(string translation)
        {
            currentTranslationText.text = translation;
            ClearWordLetters();
        }

        private void AddWordLetter(char letter)
        {
            var letterContainer = Instantiate(wordLetterPrefab, wordLettersContainer);
            letterContainer.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
        }

        private void ClearWordLetters()
        {
            for (var child = 0; child < wordLettersContainer.transform.childCount; child++)
            { Destroy(wordLettersContainer.transform.GetChild(child).gameObject); }
        }

        private void ShowPopup(PopupViewModel popupViewModel)
        {
            var popupPath = popupViewModel.PopupPath;
            var popupPrefab = Resources.Load<GameObject>(popupPath);
            var popupObject = Instantiate(popupPrefab, _uiContainer.layers[0].transform, false);
            var popup = popupObject.GetComponent<IPopup>();
            popup.Bind(popupViewModel);
            popup.OnShow();
            _createdPopupsMap.Add(popupPath, popup);
        }

        private void HidePopup(PopupViewModel popupViewModel)
        {
            if (!_createdPopupsMap.Remove(popupViewModel.PopupPath, out var popup)) return;
            popup.OnHide();
            Destroy(popup.PopupTransform.gameObject);
        }
        
        public void OnDetached() { _disposables.Dispose(); }

        public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
    }
}
