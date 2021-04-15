using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.Utilities.Cqrs.AspNetCore.Commands
{
    public interface IControllerCommandProcessor
    {
        Task<IActionResult> ProcessAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand;
    }
}