using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IDictionary<Type, ICommandHandler> _commandHandlers;

        public CommandBus()
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>(); 
        }

        public async Task SubscribeAsync<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : class, ICommand
        {
            await Task.Run(() =>
            {
                if (commandHandler == null)
                {
                    throw new ArgumentNullException(nameof(commandHandler));
                }

                var existingCommandHandler = GetCommandHandler<TCommand>();
                if (existingCommandHandler != null)
                {
                    throw new InvalidOperationException($"Command Handler already subscribed for the command {typeof(TCommand)}");
                }
                _commandHandlers.Add(typeof(TCommand), commandHandler);
            });
        }

        public async Task SendAsync<TCommand>(TCommand payload) where TCommand : class, ICommand
        {
            var commandHandler = GetCommandHandler<TCommand>();
            await ((ICommandHandler<TCommand>)commandHandler).HandleAsync(payload);
        }

        private ICommandHandler GetCommandHandler<TCommand>() where TCommand : class, ICommand
        {
            //Note: C# 7 Supports out declaration right at the point where it is passed as an out argument
            _commandHandlers.TryGetValue(typeof(TCommand), out ICommandHandler existingCommandHandler);
            return existingCommandHandler;
        }
    }
}
