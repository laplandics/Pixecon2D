using Cmd;
using R3;

namespace Game
{
    public class CmdExitGame : ICommand
    {
        public readonly Subject<Unit> ExitSignal;

        public CmdExitGame(Subject<Unit> exitSignal)
        {
            ExitSignal = exitSignal;
        }
    }
}