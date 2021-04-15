using System.Threading;
using System.Threading.Tasks;

namespace Toto.Utilities.Cqrs.Queries
{
    public interface IQueryProcessor
    {
        Task<object> ProcessAsync<TQuery>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery;
    }
}