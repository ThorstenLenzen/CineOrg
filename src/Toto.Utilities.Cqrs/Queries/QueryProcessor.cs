using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Toto.Utilities.Cqrs.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceScope _serviceScope;

        public QueryProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            if (serviceScopeFactory == null)
                throw new ArgumentNullException(nameof(serviceScopeFactory));
            
            _serviceScope = serviceScopeFactory.CreateScope();
        }
        
        public async Task<object> ProcessAsync<TQuery>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery
        {
            var queryHandlerType = typeof(IQueryHandler<TQuery>);

            var targetHandler = CreateTargetHandler<TQuery>(queryHandlerType);

            object result = await targetHandler.HandleAsync(query, cancellationToken);

            return result;
        }
        
        private dynamic CreateTargetHandler<TQuery>(Type commandHandlerType)
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
                    $"Error resolving QueryHandler '{targetHandlerInterface.Name}' for '{typeof(TQuery).Name}'.";
                throw new ProcessQueryException(message, ex);
            }

            return targetHandler;
        }
        
        #region Alternative implementation w/o need of casting...
        // public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        // {
        //     var targetHandlerInterface = typeof(IQueryHandler<,>)
        //         .MakeGenericType(query.GetType(), typeof(TResult));
        //
        //     using var scope = _serviceScopeFactory.CreateScope();
        //     
        //     dynamic targetHandler;
        //     try
        //     {
        //         targetHandler = scope.ServiceProvider.GetRequiredService(targetHandlerInterface);
        //     }
        //     catch (Exception ex)
        //     {
        //         var message =
        //             $"Error resolving QueryHandler '{targetHandlerInterface.Name}'.";
        //         throw new ProcessQueryException(message, ex);
        //     }
        //
        //     TResult result = await targetHandler.HandleAsync(query, cancellationToken);
        //
        //     return result;
        // }
        #endregion Alternative implementation w/o need of casting...
    }
}