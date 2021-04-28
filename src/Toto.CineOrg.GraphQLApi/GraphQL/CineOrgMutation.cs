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
        }

        private static QueryArguments CreateMovieArguments => 
            new (new QueryArgument<NonNullGraphType<MovieInputType>> {Name = "movie"});

        private async Task<object> ResolveCreateMovie(IResolveFieldContext<object> fieldContext)
        {
            var command = fieldContext.GetArgument<CreateMovieCommand>("movie");
            var domainMovie = await _commandProcessor.ProcessAsync(command, new CancellationToken());
            return domainMovie;
        }
    }
}