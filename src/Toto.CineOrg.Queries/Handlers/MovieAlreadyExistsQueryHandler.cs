using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries.Handlers
{
    public class MovieAlreadyExistsQueryHandler : IQueryHandler<MovieAlreadyExistsQuery>
    {
        private readonly QueryCineOrgContext _context;
        private readonly ILogger _logger;

        public MovieAlreadyExistsQueryHandler(QueryCineOrgContext context, ILogger<MovieAlreadyExistsQueryHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object> HandleAsync(MovieAlreadyExistsQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
            
            var count =  await _context.Movies.CountAsync(movie => movie.Title == query.Title, cancellationToken);
            
            _logger.LogInformation($"Movie with title '{query.Title}' has count of {count} in store.");
            
            _logger.LogDebug($"{GetType().Name} leaving.");
            
            return count != 0;
        }
    }
}