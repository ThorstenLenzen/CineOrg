using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Toto.Utilities.Cqrs.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceScope _serviceScope;

        public CommandProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            if (serviceScopeFactory == null)
                throw new ArgumentNullException(nameof(serviceScopeFactory));
            
            _serviceScope = serviceScopeFactory.CreateScope();
        }
        
        public async Task<object?> ProcessAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
        {
            var queryHandlerType = typeof(ICommandHandler<TCommand>);

            var targetHandler = CreateTargetHandler<TCommand>(queryHandlerType);

            object result = await targetHandler.HandleAsync(command, cancellationToken);

            return result;
        }
        
        private dynamic CreateTargetHandler<TCommand>(Type commandHandlerType)
        {
            var targetHandlerInterface = AppDomain
                                         .CurrentDomain
                                         .GetAssemblies()
                                         .SelectMany(assembly => assembly.GetTypes())
                                         .FirstOrDefault(type => commandHandlerType.IsAssignableFrom(type) && type.IsInterface);
            
            if (targetHandlerInterface == null) // if there is no specific interface o be found try the generic one.
                targetHandlerInterface = commandHandlerType;

            dynamic targetHandler;
            try
            {
                targetHandler = _serviceScope.ServiceProvider.GetRequiredService(targetHandlerInterface);
            }
            catch (Exception ex)
            {
                var message =
                    $"Error resolving CommandHandler '{targetHandlerInterface.Name}' for '{typeof(TCommand).Name}'.";
                throw new ProcessCommandException(message, ex);
            }

            return targetHandler;
        }
    }
}