using System;
using FluentValidation;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.ServiceModel;

namespace Toto.CineOrg.Commands.Validation
{
    public class UpdateMovieCommandValidator : AbstractValidator<MovieForUpdate>
    {
        public const int MaximumTitleLength = 100;
        
        public UpdateMovieCommandValidator()
        {
            RuleFor(movie => movie.Title)
                .NotEmpty()
                .WithMessage("Title must be set.")
                .NotNull()
                .WithMessage("Title must be set.")
                .MaximumLength(MaximumTitleLength)
                .WithMessage($"Title exceeded {MaximumTitleLength} characters.");

            RuleFor(movie => movie.Description)
                .NotEmpty()
                .WithMessage("Description must be set.")
                .NotNull()
                .WithMessage("Description must be set.");
            
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
    }
}