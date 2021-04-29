using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.DomainModel;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries.Handlers
{
    public class SeatCategoriesQueryHandler : IQueryHandler<SeatCategoriesQuery>
    {
        private readonly ILogger _logger;

        public SeatCategoriesQueryHandler(ILogger<GenresQueryHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<object> HandleAsync(SeatCategoriesQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");

            var categories = DomainSeatCategory.GetAll()
                .OrderBy(cat => cat.Key)
                .Select(cat => cat.Key)
                .ToList();

            _logger.LogInformation($"{categories.Count()} seat categories were retrieved from the system.");

            _logger.LogDebug($"{GetType().Name} leaving.");

            return Task.FromResult(categories as object);
        }
    }
}