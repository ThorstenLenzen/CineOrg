using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Cqrs.Commands;
using Toto.Utilities.Exceptions;

namespace Toto.CineOrg.Commands.Handlers
{
    public class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly CineOrgContext _context;
        private readonly ILogger _logger;

        public DeleteMovieCommandHandler(CineOrgContext context, ILogger<DeleteMovieCommandHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<object?> HandleAsync(DeleteMovieCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
            
            var orgMovie = await _context
                .Movies
                .FirstOrDefaultAsync(movie => movie.Id == command.Id, cancellationToken);

            if (orgMovie == null)
            {
                var message = $"Movie with id '{command.Id}' could not be found.";
                _logger.LogWarning(message);
                throw new NotFoundException(message);
            }
                
            _context
                .Movies
                .Remove(orgMovie);

            await _context
                .SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation($"Movie with it '{orgMovie.Id}' and title '{orgMovie.Title}' was removed from store.");
            
            _logger.LogDebug($"{GetType().Name} leaving.");
            
            return null;
        }
    }
}