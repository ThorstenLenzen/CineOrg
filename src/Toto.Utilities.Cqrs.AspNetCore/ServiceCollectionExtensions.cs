using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toto.Utilities.Cqrs.AspNetCore.Commands;
using Toto.Utilities.Cqrs.AspNetCore.Queries;
using Toto.Utilities.Cqrs.Commands;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.Utilities.Cqrs.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCreatedAtRouteControllerCommand<TCommand, TResult>(
            this IServiceCollection services,
            Action<CommandHandlerOptions<TResult>> options) where TCommand : ICommand
        {
            var optionsResult = new CommandHandlerOptions<TResult>();
            options(optionsResult);
            
            services.AddTransient<IControllerCommandHandler<TCommand>>(provider =>
                new GenericCreatedAtRouteCommandHandler<TCommand, TResult>(
                    provider.GetRequiredService<ICommandProcessor>(),
                    provider.GetRequiredService<ILogger<GenericCreatedAtRouteCommandHandler<TCommand, TResult>>>(),
                    optionsResult
                )
            );

            return services;
        }
        
        public static IServiceCollection AddCNoContentControllerCommand<TCommand>(
            this IServiceCollection services) where TCommand : ICommand
        {
            services
                .AddTransient<IControllerCommandHandler<TCommand>, GenericNoContentCommandHandler<TCommand>>();

            return services;
        }
        
        public static IServiceCollection AddControllerQuery<TQuery>(
            this IServiceCollection services,
            Action<QueryHandlerOptions> options = null) where TQuery : IQuery
        {
            QueryHandlerOptions optionsResult = null;
            if (options != null)
            {
                optionsResult = new QueryHandlerOptions();
                options(optionsResult);
            }

            services.AddTransient<IControllerQueryHandler<TQuery>>(provider =>
                new GenericControllerQueryHandler<TQuery>(
                    provider.GetRequiredService<IQueryProcessor>(),
                    provider.GetRequiredService<ILogger<GenericControllerQueryHandler<TQuery>>>(),
                    optionsResult
                ));
            
            return services;
        }
    }
}