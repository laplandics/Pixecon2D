using Cmd;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class CellLetterSetter
    {
        private readonly CurrentVocabularyHandler _currentVocabularyHandler;
        private readonly ICommandProcessor _cmd;
        
        private CompositeDisposable _disposables = new();

        public CellLetterSetter(CurrentVocabularyHandler currentVocabularyHandler,
            IObservableCollection<CellDataProxy> cells, ICommandProcessor cmd)
        {
            _currentVocabularyHandler = currentVocabularyHandler;
            _cmd = cmd;
            
            _disposables.Add(cells.ObserveAdd().Subscribe(addEvent => SetCellLetter(addEvent.Value)));
        }

        private bool SetCellLetter(CellDataProxy cell)
        {
            var word = _currentVocabularyHandler.CurrentVocabularyEntry.Value.Word.Value;
            var command = new CmdSetCellLetter(cell, word);
            var result = _cmd.Process(command);
            return result;
        }
    }
}