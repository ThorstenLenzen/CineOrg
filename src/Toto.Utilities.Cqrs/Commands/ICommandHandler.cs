using System.Threading;
using System.Threading.Tasks;

namespace Toto.Utilities.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<object?> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}