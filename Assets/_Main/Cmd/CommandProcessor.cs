using System;
using System.Collections.Generic;

namespace Cmd
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly Dictionary<Type, object> _handlesMap = new();
        
        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _handlesMap.Add(typeof(TCommand), handler);
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (!_handlesMap.TryGetValue(typeof(TCommand), out var handler)) return false;
            
            var handlerTyped = (ICommandHandler<TCommand>) handler;
            var result = handlerTyped.Handle(command);
            return result;
        }
    }
}