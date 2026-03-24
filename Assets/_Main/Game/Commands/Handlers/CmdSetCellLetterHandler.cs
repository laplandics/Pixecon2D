using Cmd;
using Settings;
using UnityEngine;

namespace Game
{
    public class CmdSetCellLetterHandler : ICommandHandler<CmdSetCellLetter>
    {
        private readonly VocabulariesSettings _vocabsSettings;
        
        public CmdSetCellLetterHandler(VocabulariesSettings vocabsSettings)
        {
            _vocabsSettings = vocabsSettings;
        }
        
        public bool Handle(CmdSetCellLetter command)
        {
            command.CellProxy.Letter.Value = _vocabsSettings.allowedWordLetters
                [Random.Range(0, _vocabsSettings.allowedWordLetters.Length)];
            
            return true;
        }
    }
}