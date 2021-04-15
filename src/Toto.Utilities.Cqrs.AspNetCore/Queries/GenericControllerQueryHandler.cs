using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.Utilities.Cqrs.AspNetCore.Queries
{
    public class GenericControllerQueryHandler<TQuery> : ControllerQueryHandlerBase<TQuery> where TQuery : IQuery
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ILogger _logger;
        private readonly QueryHandlerOptions _options;

        public GenericControllerQueryHandler(
            IQueryProcessor queryProcessor, 
            ILogger<GenericControllerQueryHandler<TQuery>> logger,
            QueryHandlerOptions options)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options;
        }
        
        public override async Task<IActionResult> HandleAsync(TQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{GetType().Name} entered.");
    
            var model = await _queryProcessor.ProcessAsync(query, cancellationToken);

            object? serviceModel = null;
            if (_options?.ConversionFunction != null)
                serviceModel = _options.ConversionFunction(model);
            
            _logger.LogDebug($"{GetType().Name} leaving.");
            
            return Ok(serviceModel ?? model);
        }
    }
}