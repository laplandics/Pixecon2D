using Cmd;
using Proxy;

namespace Game
{
    public class CmdDestroyCellHandler : ICommandHandler<CmdDestroyCell>
    {
        private readonly ProjectDataProxy _pdp;

        public CmdDestroyCellHandler(ProjectDataProxy pdp)
        {
            _pdp = pdp;
        }
        
        public bool Handle(CmdDestroyCell command)
        {
            _pdp.Cells.Remove(command.CellProxy);
            return true;
        }
    }
}