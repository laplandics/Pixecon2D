using Cmd;
using Proxy;

namespace Game
{
    public class CmdSetCellLetter : ICommand
    {
        public readonly VocabularyEntryDataProxy CurrentEntry;
        public readonly CellDataProxy CellProxy;

        public CmdSetCellLetter(CellDataProxy cellProxy, VocabularyEntryDataProxy currentEntry)
        {
            CurrentEntry = currentEntry;
            CellProxy = cellProxy;
        }
    }
}