using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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
        private readonly IValidator<CreateMovieCommand> _createMovieCommandValidator;
        private readonly IValidator<UpdateMovieCommand> _updateMovieCommandValidator;

        public CineOrgMutation(
            ICommandProcessor commandProcessor, 
            IValidator<CreateMovieCommand> createMovieCommandValidator,
            IValidator<UpdateMovieCommand> updateMovieCommandValidator)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _createMovieCommandValidator = createMovieCommandValidator ?? throw new ArgumentNullException(nameof(createMovieCommandValidator));
            _updateMovieCommandValidator = updateMovieCommandValidator ?? throw new ArgumentNullException(nameof(updateMovieCommandValidator));

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
            await _createMovieCommandValidator.ValidateAndThrowAsync(command);
            
            var domainMovie = await _commandProcessor.ProcessAsync(command, new CancellationToken());
            return domainMovie;
        }
        
        private async Task<object> ResolveUpdateMovie(IResolveFieldContext<object> fieldContext)
        {
            var command = fieldContext.GetArgument<UpdateMovieCommand>("movie");
            await _updateMovieCommandValidator.ValidateAndThrowAsync(command);
            
            var domainMovie = await _commandProcessor.ProcessAsync(command, new CancellationToken());
            return domainMovie;
        }
    }
}