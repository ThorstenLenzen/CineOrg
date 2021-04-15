using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.Utilities.Cqrs.AspNetCore.Queries
{
    public interface IControllerQueryProcessor
    {
        Task<IActionResult> ProcessAsync<TQuery>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery;
    }
}