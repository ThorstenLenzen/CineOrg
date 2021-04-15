using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries.Handlers
{
    public class MoviesQueryHandler : IQueryHandler<MoviesQuery>
    {
        private readonly QueryCineOrgContext _context;
        private readonly ILogger _logger;

        public MoviesQueryHandler(QueryCineOrgContext context, ILogger<MoviesQueryHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object> HandleAsync(MoviesQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
            
            _logger.LogDebug($"Query filter parameters are {query.Filter}.");

            var movieQuery = _context.Movies;

            switch (query.Filter.OrderBy.ToLower())
            {
                case "created":
                    movieQuery = movieQuery.OrderByDescending(movie => movie.CreatedAt);
                    break;
                case "title": 
                    movieQuery = movieQuery.OrderBy(movie => movie.Title);
                    break;
                default:
                    movieQuery = movieQuery.OrderBy(movie => movie.Title);
                    break;
            }
            
            var domainMovies = await movieQuery
                .Skip(query.Filter.Skip)
                .Take(query.Filter.Take)
                .ToListAsync(cancellationToken);
            
            _logger.LogInformation($"{domainMovies.Count} movies were retrieved from store.");
            _logger.LogDebug($"{GetType().Name} leaving.");
            
            return domainMovies;
        }
    }
}