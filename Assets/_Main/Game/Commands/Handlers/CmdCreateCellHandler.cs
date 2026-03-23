using Cmd;
using Proxy;

namespace Game
{
    public class CmdCreateCellHandler : ICommandHandler<CmdCreateCell>
    {
        private readonly ProjectDataProxy _pdp;

        public CmdCreateCellHandler(ProjectDataProxy pdp)
        {
            _pdp = pdp;
        }
        
        public bool Handle(CmdCreateCell command)
        {
            var newCellData = new Data.CellData
            {
                entityID = _pdp.GetGlobalEntityId,
                key = Constant.Names.EntityData.CELL_EMPTY_KEY,
                position = command.Position
            };
            var newCellProxy = new CellDataProxy(newCellData);
            _pdp.Cells.Add(newCellProxy);
            
            return true;
        }
    }
}