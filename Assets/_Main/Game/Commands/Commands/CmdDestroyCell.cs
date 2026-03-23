using Cmd;
using Proxy;

namespace Game
{
    public class CmdDestroyCell : ICommand
    {
        public readonly CellDataProxy CellProxy;
        
        public CmdDestroyCell(CellDataProxy cellProxy)
        {
            CellProxy = cellProxy;
        }
    }
}