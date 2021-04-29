using Microsoft.Extensions.DependencyInjection;
using Toto.CineOrg.Commands;
using Toto.CineOrg.Commands.Converters;
using Toto.CineOrg.Commands.Handlers;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Queries;
using Toto.CineOrg.Queries.Handlers;
using Toto.CineOrg.ServiceModel.Converters;
using Toto.Utilities.Cqrs.AspNetCore;
using Toto.Utilities.Cqrs.AspNetCore.Commands;
using Toto.Utilities.Cqrs.AspNetCore.Queries;
using Toto.Utilities.Cqrs.Commands;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Hosting
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCqrsInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IControllerCommandProcessor, ControllerCommandProcessor>();
            services.AddScoped<IControllerQueryProcessor, ControllerQueryProcessor>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<ICommandProcessor, CommandProcessor>();

            return services;
        }
        
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<DeleteMovieCommand>, DeleteMovieCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateMovieCommand>, UpdateMovieCommandHandler>();
            services.AddTransient<ICommandHandler<CreateMovieCommand>, CreateMovieCommandHandler>();

            return services;
        }
        
        public static IServiceCollection AddControllerCommands(this IServiceCollection services)
        {
            // Todo: Make automatic...
            services.AddCNoContentControllerCommand<DeleteMovieCommand>();
            services.AddCNoContentControllerCommand<UpdateMovieCommand>();
            services.AddCreatedAtRouteControllerCommand<CreateMovieCommand, DomainMovie>(options =>
            {
                options.RouteName = "GetMovieByIdAsync";
                options.IdProperty = movie => movie.Id;
                options.ConversionFunction = ServiceModelConverter.ConvertDomainMovieToMovieDto;;
            });

            return services;
        }
        
        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<MovieQuery>, MovieQueryHandler>();
            services.AddTransient<IQueryHandler<MoviesQuery>, MoviesQueryHandler>();
            services.AddTransient<IQueryHandler<MovieAlreadyExistsQuery>, MovieAlreadyExistsQueryHandler>();
            services.AddTransient<IQueryHandler<GenresQuery>, GenresQueryHandler>();
            services.AddTransient<IQueryHandler<TheatresQuery>, TheatresQueryHandler>();
            services.AddTransient<IQueryHandler<SeatsQuery>, SeatsQueryHandler>();
            services.AddTransient<IQueryHandler<SeatCategoriesQuery>, SeatCategoriesQueryHandler>();

            return services;
        }
        
        public static IServiceCollection AddControllerQueries(this IServiceCollection services)
        {
            // Todo: Make automatic...
            services.AddControllerQuery<MovieQuery>(options =>
            {
                options.ConversionFunction = ServiceModelConverter.ConvertDomainMovieToMovieDto;
            });

            services.AddControllerQuery<MoviesQuery>(options =>
            {
                options.ConversionFunction = ServiceModelConverter.ConvertDomainMovieListToMovieListDto;
            });
            
            services.AddControllerQuery<GenresQuery>();
            
            return services;
        }
    }
}