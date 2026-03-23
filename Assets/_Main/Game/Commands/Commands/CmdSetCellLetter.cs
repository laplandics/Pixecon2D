using Cmd;
using Proxy;

namespace Game
{
    public class CmdSetCellLetter : ICommand
    {
        public readonly CellDataProxy CellProxy;

        public CmdSetCellLetter(CellDataProxy cellProxy)
        {
            CellProxy = cellProxy;
        }
    }
}