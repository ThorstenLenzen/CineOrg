using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Queries;
using Toto.Utilities.Exceptions;

namespace Toto.CineOrg.Queries.Handlers
{
    public class MovieQueryHandler : IQueryHandler<MovieQuery>
    {
        private readonly QueryCineOrgContext _context;
        private readonly ILogger _logger;

        public MovieQueryHandler(QueryCineOrgContext context, ILogger<MovieQueryHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object> HandleAsync(MovieQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
            
            var domainMovie = await _context.FindAsync<DomainMovie>(query.Id);
            
            if(domainMovie == null)
            {
                var message = $"Movie with id '{query.Id}' could not be found.";
                _logger.LogWarning(message);
                throw  new NotFoundException(message);
            }
            
            _logger.LogInformation($"Movie with it '{domainMovie.Id}' and title '{domainMovie.Title}' was retrieved from store.");
            
            // var movie = domainMovie.ConvertToDto();
            
            _logger.LogDebug($"{GetType().Name} leaving.");
            
            return domainMovie;
        }
    }
}