using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.Utilities.Cqrs.AspNetCore.Commands
{
    public interface IControllerCommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<IActionResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}