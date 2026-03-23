using Cmd;
using Proxy;

namespace Game
{
    public class CmdHandleCellClick : ICommand
    {
        public CellDataProxy CellProxy;

        public CmdHandleCellClick(CellDataProxy cellProxy)
        {
            CellProxy = cellProxy;
        }
    }
}