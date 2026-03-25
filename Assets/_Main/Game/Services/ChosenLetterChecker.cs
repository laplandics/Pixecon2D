using GameView;
using R3;

namespace Game
{
    public class ChosenLetterChecker
    {
        private readonly CurrentVocabularyHandler _currentVocabularyHandler;
        private int _currentEnteredWordLetterIndex;
        
        public ChosenLetterChecker(CurrentVocabularyHandler currentVocabularyHandler)
        {
            _currentVocabularyHandler = currentVocabularyHandler;
            _currentVocabularyHandler.CurrentVocabularyEntry.Subscribe(_ => _currentEnteredWordLetterIndex = 0);
        }
        
        public bool CheckLetter(CellViewModel checkingCell)
        {
            var letter = checkingCell.Letter.CurrentValue;
            var currentEntry = _currentVocabularyHandler.CurrentVocabularyEntry.Value;
            var currentWord = currentEntry.Word.Value;
            
            if (currentWord[_currentEnteredWordLetterIndex] != letter) return false;
            if (!_currentVocabularyHandler.ChangeLastCorrectLetter(_currentEnteredWordLetterIndex)) return true;
            
            _currentEnteredWordLetterIndex++;
            return true;
        }
    }
}