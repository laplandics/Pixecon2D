using Cmd;
using Proxy;

namespace Game
{
    public class CmdCheckLetterHandler : ICommandHandler<CmdCheckLetter>
    {
        private readonly VocabularyEntryDataProxy _currentEntryDataProxy;
        private int _currentLetterIndex;

        public CmdCheckLetterHandler(VocabularyEntryDataProxy currentEntryDataProxy)
        {
            _currentEntryDataProxy = currentEntryDataProxy;
            _currentLetterIndex = 0;
        }

        public bool Handle(CmdCheckLetter command)
        {
            var checkingCell = command.CheckingCell;
            
            var currentWord = _currentEntryDataProxy.Word.Value;
            if (checkingCell.Letter.CurrentValue != currentWord[_currentLetterIndex]) return false;
            
            _currentLetterIndex++;
            _currentEntryDataProxy.LastEnteredLetterIndex.Value++;
            
            if (currentWord.Length <= _currentLetterIndex) _currentEntryDataProxy.IsDone.Value = true;
            return true;
        }
    }
}