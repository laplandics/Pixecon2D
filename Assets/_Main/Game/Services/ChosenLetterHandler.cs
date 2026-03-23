using GameView;
using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class ChosenLetterHandler
    {
        private VocabularyEntryDataProxy _currentEntryDataProxy;
        private int _currentLetterIndex;
        
        public ChosenLetterHandler(CellBuilder cellBuilder, ObservableList<VocabularyDataProxy> vocabulariesProxy)
        {
            foreach (var vocabularyDataProxy in vocabulariesProxy)
            {
                foreach (var entryDataProxy in vocabularyDataProxy.VocabularyEntries)
                { entryDataProxy.IsCurrent.Subscribe(current =>
                    { if (current) UpdateCurrentEntry(entryDataProxy); }); }
            }

            cellBuilder.AllCells.ObserveRemove().Subscribe(removeEvent => CheckRemovedLetter(removeEvent.Value));
        }

        private void UpdateCurrentEntry(VocabularyEntryDataProxy newEntryDataProxy)
        {
            _currentEntryDataProxy = newEntryDataProxy;
            _currentLetterIndex = 0;
        }

        private void CheckRemovedLetter(CellViewModel removedCell)
        {
            var currentWord = _currentEntryDataProxy.Word.Value.ToLower();
            if (removedCell.Letter.CurrentValue == currentWord[_currentLetterIndex])
            {
                _currentLetterIndex++;
                _currentEntryDataProxy.LastEnteredLetter.Value = removedCell.Letter.CurrentValue;
                if (currentWord.Length <= _currentLetterIndex)
                {
                    _currentEntryDataProxy.IsDone.Value = true;
                }
            }
            else
            {
                Debug.Log("Letter is not correct");
            }
        }
    }
}