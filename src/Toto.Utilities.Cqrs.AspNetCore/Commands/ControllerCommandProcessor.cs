using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.Utilities.Cqrs.AspNetCore.Commands
{
    public class ControllerCommandProcessor : IControllerCommandProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ControllerCommandProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }
        
        public async Task<IActionResult> ProcessAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
        {
            var commandHandlerType = typeof(IControllerCommandHandler<TCommand>);

            var targetHandlerInterface = AppDomain
                 .CurrentDomain
                 .GetAssemblies()
                 .SelectMany(assembly => assembly.GetTypes())
                 .FirstOrDefault(type => commandHandlerType.IsAssignableFrom(type) && type.IsInterface);

            if (targetHandlerInterface == null) // if there is no specific interface o be found try the generic one.
                targetHandlerInterface = commandHandlerType;

            using var scope = _serviceScopeFactory.CreateScope();
            
            dynamic targetHandler;
            try
            {
                targetHandler = scope.ServiceProvider.GetRequiredService(targetHandlerInterface);
            }
            catch (Exception ex)
            {
                var message =
                    $"Error resolving CommandHandler '{targetHandlerInterface.Name}' for '{typeof(TCommand).Name}'.";
                throw new ProcessCommandException(message, ex);
            }
            
            IActionResult result = await targetHandler.HandleAsync(command, cancellationToken);
            return result;
        }
    }
}