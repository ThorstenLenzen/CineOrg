using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.Utilities.Cqrs.AspNetCore.Commands
{
    public abstract class ControllerCommandHandlerBase<TCommand> : IControllerCommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract Task<IActionResult> HandleAsync(TCommand command, CancellationToken cancellationToken);

        protected NoContentResult NoContent()
            => new NoContentResult();
        
        protected CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, object value)
            => new CreatedAtRouteResult(routeName, routeValues, value);
    }
}