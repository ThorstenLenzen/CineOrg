using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Queries;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Commands.Validation
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        private readonly IQueryProcessor _processor;

        public CreateMovieCommandValidator(IQueryProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));

            RuleFor(movie => movie.Title)
                .NotEmpty()
                .WithMessage("Title must be set.")
                .NotNull()
                .WithMessage("Title must be set.")
                .MaximumLength(DomainMovie.MaximumTitleLength)
                .WithMessage($"Title exceeded {DomainMovie.MaximumTitleLength} characters.")
                .MustAsync(MovieMustNotAlreadyExist)
                .WithMessage("Movie does already exist.");

            RuleFor(movie => movie.Description)
                .NotEmpty()
                .WithMessage("Description must be set.")
                .NotNull()
                .WithMessage("Description must be set.")
                .MaximumLength(DomainMovie.MaximumDescriptionLength)
                .WithMessage($"Description exceeded {DomainMovie.MaximumDescriptionLength} characters.");

            RuleFor(movie => movie.Genre)
                .NotEmpty()
                .WithMessage("Genre must be set.")
                .NotNull()
                .WithMessage("Genre must be set.")
                .Must(genre => genre != null && DomainGenre.Get(genre).IsValid)
                .WithMessage(movie => $"Genre {movie.Genre} is not a valid value.");

            RuleFor(movie => movie.YearReleased)
                .Must(year => year <= DateTime.UtcNow.Year)
                .WithMessage("Year of release may not be in the future.");
        }

        private async Task<bool> MovieMustNotAlreadyExist(string title, CancellationToken cancellationToken)
        {
            var query = new MovieAlreadyExistsQuery {Title = title};
            var movieExists = (bool)await _processor.ProcessAsync(query, cancellationToken);
            
            return !movieExists;
        }
    }
}