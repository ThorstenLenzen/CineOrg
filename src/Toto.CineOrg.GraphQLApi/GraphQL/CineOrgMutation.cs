using System;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Toto.CineOrg.Commands;
using Toto.CineOrg.GraphQLApi.GraphQL.Types;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.CineOrg.GraphQLApi.GraphQL
{
    public class CineOrgMutation : ObjectGraphType
    {
        private readonly ICommandProcessor _commandProcessor;

        public CineOrgMutation(ICommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            
            FieldAsync<MovieQueryType>("createMovie", arguments: CreateMovieArguments, resolve: ResolveCreateMovie);
            FieldAsync<MovieQueryType>("updateMovie", arguments: UpdateMovieArguments, resolve: ResolveUpdateMovie);
        }

        private static QueryArguments CreateMovieArguments => 
            new (new QueryArgument<NonNullGraphType<MovieCreateInputType>> {Name = "movie"});
        
        private static QueryArguments UpdateMovieArguments => 
            new (new QueryArgument<NonNullGraphType<MovieUpdateInputType>> {Name = "movie"});

        private async Task<object> ResolveCreateMovie(IResolveFieldContext<object> fieldContext)
        {
            var command = fieldContext.GetArgument<CreateMovieCommand>("movie");
            var domainMovie = await _commandProcessor.ProcessAsync(command, new CancellationToken());
            return domainMovie;
        }
        
        private async Task<object> ResolveUpdateMovie(IResolveFieldContext<object> fieldContext)
        {
            var command = fieldContext.GetArgument<UpdateMovieCommand>("movie"); 
            var domainMovie = await _commandProcessor.ProcessAsync(command, new CancellationToken());
            return domainMovie;
        }
    }
}