using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.Utilities.Cqrs.AspNetCore.Commands
{
    public class GenericNoContentCommandHandler<TCommand> : ControllerCommandHandlerBase<TCommand> where TCommand : ICommand
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly ILogger _logger;
    
        public GenericNoContentCommandHandler(ICommandProcessor commandProcessor, ILogger<GenericNoContentCommandHandler<TCommand>> logger)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public override async Task<IActionResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
    
            var _ = await _commandProcessor.ProcessAsync(command, cancellationToken);
            
            _logger.LogDebug($"{GetType().Name} leaving.");
            
            return NoContent();
        }
    }
}