using Cmd;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class CellLetterSetter
    {
        private readonly ICommandProcessor _cmd;
        private VocabularyEntryDataProxy _currentEntry;

        public CellLetterSetter(IObservableCollection<CellDataProxy> cells,
            IObservableCollection<VocabularyDataProxy> vocabularies, ICommandProcessor cmd)
        {
            _cmd = cmd;

            foreach (var vocabulary in vocabularies)
            {
                foreach (var vocabularyEntry in vocabulary.VocabularyEntries)
                { vocabularyEntry.IsCurrent.Subscribe(current 
                    => { if (current) { _currentEntry = vocabularyEntry; } }); }
            }
            
            foreach (var cellProxy in cells) { SetLetter(cellProxy); }
            cells.ObserveAdd().Subscribe(addEvent => { SetLetter(addEvent.Value); });
        }

        private bool SetLetter(CellDataProxy cellProxy)
        {
            var setLetterCommand = new CmdSetCellLetter(cellProxy, _currentEntry);
            var result = _cmd.Process(setLetterCommand);
            
            return result;
        }
    }
}