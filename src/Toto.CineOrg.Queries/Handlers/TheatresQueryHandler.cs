using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries.Handlers
{
    public class TheatresQueryHandler : IQueryHandler<TheatresQuery>
    {
        private readonly QueryCineOrgContext _context;
        private readonly ILogger _logger;

        public TheatresQueryHandler(QueryCineOrgContext context, ILogger<MovieAlreadyExistsQueryHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object> HandleAsync(TheatresQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");

            var domainTheatres = await _context.Theatres.ToListAsync(cancellationToken);
            
            _logger.LogInformation($"{domainTheatres.Count} theatres were retrieved from store.");
            _logger.LogDebug($"{GetType().Name} leaving.");

            return domainTheatres;
        }
    }
}