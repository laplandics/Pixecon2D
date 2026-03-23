using Cmd;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class CellLetterSetter
    {
        private readonly ICommandProcessor _cmd;

        public CellLetterSetter(IObservableCollection<CellDataProxy> cells, ICommandProcessor cmd)
        {
            _cmd = cmd;
            foreach (var cellProxy in cells) { SetLetter(cellProxy); }

            cells.ObserveAdd().Subscribe(addEvent => { SetLetter(addEvent.Value); });
        }

        private bool SetLetter(CellDataProxy cellProxy)
        {
            var setLetterCommand = new CmdSetCellLetter(cellProxy);
            var result = _cmd.Process(setLetterCommand);
            
            return result;
        }
    }
}