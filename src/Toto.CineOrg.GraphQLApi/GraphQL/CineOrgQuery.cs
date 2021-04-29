using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.GraphQLApi.GraphQL.Types;
using Toto.CineOrg.Queries;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.GraphQLApi.GraphQL
{
    public class CineOrgQuery :  ObjectGraphType
    {
        private readonly IQueryProcessor _queryProcessor;

        public CineOrgQuery(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            
            FieldAsync<ListGraphType<MovieQueryType>>("movies", arguments: MoviesQueryArguments, resolve: ResolveMoviesQueryAsync);
            FieldAsync<MovieQueryType>("movie", arguments: MovieQueryArguments, resolve: ResolveMovieQueryAsync);
            FieldAsync<ListGraphType<GenreQueryType>>("genres", resolve: ResolveGenresQueryAsync);
            FieldAsync<ListGraphType<SeatCategoryQueryType>>("seatCategories", resolve: ResolveSeatCategoriesQueryAsync);
            FieldAsync<ListGraphType<TheatreQueryType>>("theatres", resolve: ResolveTheatresQueryAsync);
            FieldAsync<ListGraphType<SeatQueryType>>("seats", resolve: ResolveSeatsQueryAsync);
        }

        private static QueryArguments MovieQueryArguments =>
            new(new QueryArgument<NonNullGraphType<IdGraphType>>{Name = "id"});

        private static QueryArguments MoviesQueryArguments =>
            new (new QueryArgument<MoviesQueryFilterType> {Name = "filter"});
        
        private async Task<object> ResolveMoviesQueryAsync(IResolveFieldContext<object> fieldContext)
        {
            var query = new MoviesQuery
            {
                Filter = fieldContext.GetArgument<MoviesQueryFilter>("filter") ?? new MoviesQueryFilter()
            };
            
            var movies = await _queryProcessor
                .ProcessAsync(query, new CancellationToken());

            return movies as IList<DomainMovie>;
        }

        private async Task<object> ResolveMovieQueryAsync(IResolveFieldContext<object> fieldContext)
        {
            var query = new MovieQuery
            {
                Id = fieldContext.GetArgument<Guid>("id")
            };

            var movie =  await _queryProcessor.ProcessAsync(query, new CancellationToken());
            
            return movie as DomainMovie;
        }

        private async Task<object> ResolveGenresQueryAsync(IResolveFieldContext<object> fieldContext)
        {
            var genreNames = await _queryProcessor
                .ProcessAsync(new GenresQuery(), new CancellationToken());

            return genreNames as List<string>;
        }
        
        private async Task<object> ResolveSeatCategoriesQueryAsync(IResolveFieldContext<object> arg)
        {
            var categoryNames = await _queryProcessor
                .ProcessAsync(new SeatCategoriesQuery(), new CancellationToken());

            return categoryNames as List<string>;
        }
        
        private async Task<object> ResolveTheatresQueryAsync(IResolveFieldContext<object> fieldContext)
        {
            var theatres = await _queryProcessor
                .ProcessAsync(new TheatresQuery(), new CancellationToken());

            return theatres as IList<DomainTheatre>;
        }
        
        private async Task<object> ResolveSeatsQueryAsync(IResolveFieldContext<object> fieldContext)
        {
            var theatres = await _queryProcessor
                .ProcessAsync(new SeatsQuery(), new CancellationToken());

            return theatres as IList<DomainSeat>;
        }
    }
}