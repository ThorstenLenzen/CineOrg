using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.Utilities.Cqrs.AspNetCore.Queries
{
    public interface IControllerQueryHandler<in TQuery> where TQuery : IQuery
    {
        Task<IActionResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}