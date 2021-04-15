using System.Threading;
using System.Threading.Tasks;

namespace Toto.Utilities.Cqrs.Commands
{
    public interface ICommandProcessor
    {
        Task<object?> ProcessAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;
    }
}