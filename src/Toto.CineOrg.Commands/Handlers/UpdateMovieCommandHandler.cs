using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Commands.Converters;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Commands;
using Toto.Utilities.Exceptions;

namespace Toto.CineOrg.Commands.Handlers
{
    public class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly CineOrgContext _context;
        private readonly ILogger _logger;

        public UpdateMovieCommandHandler(CineOrgContext context, ILogger<UpdateMovieCommandHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object?> HandleAsync(UpdateMovieCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
            
            var newMovie = command.ConvertToDomain();

            var domainMovie = await _context.FindAsync<DomainMovie>(newMovie.Id);
            
            if(domainMovie == null)
            {
                var message = $"Movie with id '{newMovie.Id}' could not be found.";
                _logger.LogWarning(message);
                throw new NotFoundException(message);
            }

            domainMovie.CopyValuesFrom(newMovie);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation($"Movie with it '{newMovie.Id}' and title '{newMovie.Title}' was updated in store.");
            
            _logger.LogDebug($"{GetType().Name} leaving.");

            return null;
        }
    }
}