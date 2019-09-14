using System.Threading.Tasks;

namespace Infrastructure.Core.Command
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}