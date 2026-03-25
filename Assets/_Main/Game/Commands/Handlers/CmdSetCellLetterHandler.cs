using System.Collections.Generic;
using Cmd;
using Settings;
using UnityEngine;

namespace Game
{
    public class CmdSetCellLetterHandler : ICommandHandler<CmdSetCellLetter>
    {
        private readonly VocabulariesSettings _vocabSettings;
        private readonly Dictionary<Vector2, Letter> _lettersMap;
        private readonly List<Letter> _positionedWordLetters;
        private string _lastWord;
        
        public CmdSetCellLetterHandler(VocabulariesSettings vocabSettings)
        {
            _vocabSettings = vocabSettings;
            _positionedWordLetters = new List<Letter>();
            _lettersMap = new Dictionary<Vector2, Letter>();
        }
        
        public bool Handle(CmdSetCellLetter command)
        {
            var word = command.CurrentWord;
            if (_lastWord != word && !string.IsNullOrEmpty(_lastWord))
            {
                _positionedWordLetters.Clear();
                foreach (var lettersMapValue in _lettersMap.Values)
                { lettersMapValue.IsWordLetter = false; lettersMapValue.Index = -1; }
            }
            _lastWord = word;
            
            var index = 0;
            var wordLetters = new List<Letter>();
            foreach (var letter in word)
            {
                var wordLetter = new Letter();
                wordLetter.IsWordLetter = true;
                wordLetter.Value = letter;
                wordLetter.Index = index;
                wordLetters.Add(wordLetter);
                index++;
            }
            
            var wordIndexesToRemove = new List<int>();
            foreach (var letter in wordLetters)
            {
                foreach (var positionedWordLetter in _positionedWordLetters)
                {
                    if (positionedWordLetter.Value != letter.Value
                        || positionedWordLetter.Index != letter.Index) continue;
                    
                    if (!wordIndexesToRemove.Contains(positionedWordLetter.Index))
                    { wordIndexesToRemove.Add(letter.Index); }
                }
                
                foreach (var mapLetter in _lettersMap.Values)
                {
                    if (!mapLetter.IsWordLetter ||
                        mapLetter.Value != letter.Value ||
                        mapLetter.Index != letter.Index) continue;
                    
                    if (!wordIndexesToRemove.Contains(mapLetter.Index))
                    { wordIndexesToRemove.Add(letter.Index); }
                }
            }

            foreach (var wordLetter in wordLetters)
            {
                var wordCharOnMap = false;
                var wordChar = wordLetter.Value;
                
                foreach (var letterMapValue in _lettersMap.Values)
                {
                    var mapChar = letterMapValue.Value;
                    if (mapChar != wordChar) continue;
                    wordCharOnMap = true; break;
                }
                
                if (!wordCharOnMap && wordIndexesToRemove.Contains(wordLetter.Index))
                { wordIndexesToRemove.Remove(wordLetter.Index); }
            }
            
            foreach (var indexToRemove in wordIndexesToRemove)
            { wordLetters.Remove(wordLetters.Find(l => l.Index == indexToRemove)); }

            if (wordLetters.Count == 0)
            {
                var rLetter = new Letter();
                rLetter.IsWordLetter = false;
                rLetter.Value = GetRandomLetter;
                rLetter.Index = -1;
                _lettersMap[command.Cell.Position.CurrentValue] = rLetter;
                command.Cell.Letter.Value = rLetter.Value;
                return true;
            }

            var wLetter = wordLetters[0];
            _lettersMap[command.Cell.Position.CurrentValue] = wLetter;
            _positionedWordLetters.Add(wLetter);
            command.Cell.Letter.Value = wLetter.Value;
            
            return true;
        }
        
        private char GetRandomLetter => _vocabSettings.allowedWordLetters
            [Random.Range(0, _vocabSettings.allowedWordLetters.Length)];

        private class Letter
        { public bool IsWordLetter; public char Value; public int Index; }
    }
}