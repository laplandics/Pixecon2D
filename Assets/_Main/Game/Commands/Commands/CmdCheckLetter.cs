using Cmd;
using GameView;

namespace Game
{
    public class CmdCheckLetter : ICommand
    {
        public readonly CellViewModel CheckingCell;

        public CmdCheckLetter(CellViewModel checkingCell)
        {
            CheckingCell = checkingCell;
        }
    }
}