using System.Threading;
using System.Threading.Tasks;

namespace Toto.Utilities.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery> where TQuery : IQuery
    {
        Task<object> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}