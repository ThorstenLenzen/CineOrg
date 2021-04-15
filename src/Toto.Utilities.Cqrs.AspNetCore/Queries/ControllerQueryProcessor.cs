using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.Utilities.Cqrs.AspNetCore.Queries
{
    public class ControllerQueryProcessor : IControllerQueryProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ControllerQueryProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }
        
        public async Task<IActionResult> ProcessAsync<TQuery>(TQuery command, CancellationToken cancellationToken) where TQuery : IQuery
        {
            var queryHandlerType = typeof(IControllerQueryHandler<TQuery>);

            var targetHandlerInterface = AppDomain
                 .CurrentDomain
                 .GetAssemblies()
                 .SelectMany(assembly => assembly.GetTypes())
                 .FirstOrDefault(type => queryHandlerType.IsAssignableFrom(type) && type.IsInterface);

            if (targetHandlerInterface == null) // if there is no specific interface o be found try the generic one.
                targetHandlerInterface = queryHandlerType;

            using var scope = _serviceScopeFactory.CreateScope();
            
            dynamic targetHandler;
            try
            {
                targetHandler = scope.ServiceProvider.GetRequiredService(targetHandlerInterface);
            }
            catch (Exception ex)
            {
                var message =
                    $"Error resolving QueryHandler '{targetHandlerInterface.Name}' for '{typeof(TQuery).Name}'.";
                throw new ProcessQueryException(message, ex);
            }
            
            IActionResult result = await targetHandler.HandleAsync(command, cancellationToken);
            return result;
        }
    }
}