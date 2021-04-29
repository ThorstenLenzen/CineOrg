using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.DomainModel;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries.Handlers
{
    public class GenresQueryHandler : IQueryHandler<GenresQuery>
    {
        private readonly ILogger _logger;

        public GenresQueryHandler(ILogger<GenresQueryHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<object> HandleAsync(GenresQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");

            var genres = DomainGenre.GetAll()
                                    .OrderBy(genre => genre.Key)
                                    .Select(genre => genre.Key)
                                    .ToList();

            _logger.LogInformation($"{genres.Count()} genres were retrieved from the system.");

            _logger.LogDebug($"{GetType().Name} leaving.");

            return Task.FromResult(genres as object);
        }
    }
}