using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Commands.Converters;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.CineOrg.Commands.Handlers
{
    public class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand>
    {
        private readonly CineOrgContext _context;
        private readonly ILogger _logger;

        public CreateMovieCommandHandler(CineOrgContext context, ILogger<CreateMovieCommandHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object?> HandleAsync(CreateMovieCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
            
            var domainMovie = command.ConvertToDomain();

            _context.Movies.Add(domainMovie);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation($"Movie with it '{domainMovie.Id}' and title '{domainMovie.Title}' was created in store.");

            _logger.LogDebug($"{GetType().Name} leaving.");

            return domainMovie;
        }
    }
}