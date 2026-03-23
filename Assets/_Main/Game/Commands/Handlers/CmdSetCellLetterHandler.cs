using System.Collections.Generic;
using Cmd;
using Settings;
using UnityEngine;

namespace Game
{
    public class CmdSetCellLetterHandler : ICommandHandler<CmdSetCellLetter>
    {
        private readonly VocabulariesSettings _vocabsSettings;
        private readonly Dictionary<Vector2, char> _lettersOnPositionMap;
        
        public CmdSetCellLetterHandler(VocabulariesSettings vocabsSettings)
        {
            _vocabsSettings = vocabsSettings;
            _lettersOnPositionMap = new Dictionary<Vector2, char>();
        }
        
        public bool Handle(CmdSetCellLetter command)
        {
            Handle:
            
            var cellProxy = command.CellProxy;
            var letter = _vocabsSettings.allowedWordLetters
                [Random.Range(0, _vocabsSettings.allowedWordLetters.Length)];
            
            if (_lettersOnPositionMap.TryAdd(cellProxy.Position.Value, letter))
            { cellProxy.Letter.Value = letter; return true; }

            if (_lettersOnPositionMap[cellProxy.Position.Value] != letter)
            { _lettersOnPositionMap[cellProxy.Position.Value] = letter; 
                cellProxy.Letter.Value = letter; return true; }
            
            goto Handle;
        }
    }
}