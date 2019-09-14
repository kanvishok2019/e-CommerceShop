using System.Threading.Tasks;

namespace Infrastructure.Core.Command
{
    public interface ICommandBus
    {
        Task SubscribeAsync<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : class, ICommand;

        Task SendAsync<TCommand>(TCommand payload) where TCommand : class, ICommand;

    }
}