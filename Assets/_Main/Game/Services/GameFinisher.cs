using Cmd;
using R3;

namespace Game
{
    public class GameFinisher
    {
        private readonly ICommandProcessor _cmd;
        private readonly Subject<Unit> _exitSignal;

        public GameFinisher(ICommandProcessor cmd, Subject<Unit> exitSignal)
        {
            _exitSignal = exitSignal;
            _cmd = cmd;
        }

        public void FinishGame()
        {
            _cmd.Process(new CmdExitGame(_exitSignal));
        }
    }
}