using Cmd;
using R3;

namespace Game
{
    public class CmdExitGameHandler : ICommandHandler<CmdExitGame>
    {
        public bool Handle(CmdExitGame command)
        {
            command.ExitSignal.OnNext(Unit.Default);
            return true;
        }
    }
}