using Game;
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
        
        private readonly CompositeDisposable _disposables = new();
        private GameFinisher _gameFinisher;
        
        public void Bind(GameUIRootViewModel vm)
        {
            _gameFinisher = vm.Finisher;

            foreach (var entryDataProxy in vm.VocabularyEntries)
            {
                _disposables.Add(entryDataProxy.IsCurrent.Subscribe(isCurrent =>
                    { if (isCurrent) ChangeTranslation(entryDataProxy.Translation.Value); } ));
                _disposables.Add(entryDataProxy.LastEnteredLetter.Skip(1).Subscribe(AddWordLetter));
            }
        }

        public void OnAttached()
        {
            AssignButtons();
        }

        private void AssignButtons()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }
        
        private void OnPauseButtonClicked()
        {
            _gameFinisher.FinishGame();
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
        
        public void OnRemoved() { _disposables.Dispose(); }

        public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
    }
}
