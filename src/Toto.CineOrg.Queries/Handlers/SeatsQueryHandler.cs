using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries.Handlers
{
    public class SeatsQueryHandler : IQueryHandler<SeatsQuery>
    {
        private readonly QueryCineOrgContext _context;
        private readonly ILogger _logger;

        public SeatsQueryHandler(QueryCineOrgContext context, ILogger<MovieAlreadyExistsQueryHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object> HandleAsync(SeatsQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");

            var domainSeats = await _context.Seats.ToListAsync(cancellationToken);
            
            _logger.LogInformation($"{domainSeats.Count} seats were retrieved from store.");
            _logger.LogDebug($"{GetType().Name} leaving.");

            return domainSeats;
        }
    }
}