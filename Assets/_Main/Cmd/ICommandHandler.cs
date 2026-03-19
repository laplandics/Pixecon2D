namespace Cmd
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        public bool Handle(TCommand command);
    }
}