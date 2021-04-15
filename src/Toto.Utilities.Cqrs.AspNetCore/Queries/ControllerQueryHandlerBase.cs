using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.Utilities.Cqrs.AspNetCore.Queries
{
    public abstract class ControllerQueryHandlerBase<TQuery> : IControllerQueryHandler<TQuery> where TQuery : IQuery
    {
        public abstract Task<IActionResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
        
        protected OkObjectResult Ok(object value)
            => new OkObjectResult(value);

        protected NoContentResult NoContent()
            => new NoContentResult();
    }
}