using Cmd;

namespace Game
{
    public class CmdHandleCellClickHandler : ICommandHandler<CmdHandleCellClick>
    {
        private readonly CellBuilder _cellBuilder;

        public CmdHandleCellClickHandler(CellBuilder cellBuilder)
        {
            _cellBuilder = cellBuilder;
        }
        
        public bool Handle(CmdHandleCellClick command)
        {
            return _cellBuilder.RemoveCell(command.CellProxy);
        }
    }
}