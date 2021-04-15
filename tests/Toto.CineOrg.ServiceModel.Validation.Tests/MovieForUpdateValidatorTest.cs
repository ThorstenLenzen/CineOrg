using System;
using FluentValidation.TestHelper;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.ServiceModel.Validation.Tests
{
    public class MovieForUpdateValidatorTest
    {
        private readonly MovieForUpdateValidator _validator;
        private readonly MovieForUpdate _movie;

        public MovieForUpdateValidatorTest()
        {
            _validator = new MovieForUpdateValidator();
            _movie = DataStructures.ValidMovieForUpdate;
        }

        [Fact]
        public void Can_Validate_Valid_Movie()
        {
            _validator.TestValidate(_movie);
        }
        
        [Fact]
        public void Can_Not_Validate_Title_When_Null()
        {
            _movie.Title = null;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Title)
                  .WithErrorMessage("Title must be set.");
        }
        
        [Fact]
        public void Can_Not_Validate_Title_When_Empty()
        {
            _movie.Title = string.Empty;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Title)
                  .WithErrorMessage("Title must be set.");
        }
        
        [Fact]
        public void Can_Not_Validate_Title_When_Too_Long()
        {
            _movie.Title = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength + 1);
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Title)
                  .WithErrorMessage($"Title exceeded {DomainMovie.MaximumTitleLength} characters.");
        }

        [Fact]
        public void Can_Not_Validate_Description_When_Null()
        {
            _movie.Description = null;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Description)
                  .WithErrorMessage("Description must be set.");
        }
        
        [Fact]
        public void Can_Not_Validate_Description_When_Empty()
        {
            _movie.Description = string.Empty;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Description)
                  .WithErrorMessage("Description must be set.");
        }
        
        [Fact]
        public void Can_Not_Validate_Genre_When_Null()
        {
            _movie.Genre = null;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Genre)
                  .WithErrorMessage("Genre must be set.");
        }
        
        [Fact]
        public void Can_Not_Validate_Genre_When_Empty()
        {
            _movie.Genre = string.Empty;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Genre)
                  .WithErrorMessage("Genre must be set.");
        }
        
        [Fact]
        public void Can_Not_Validate_Genre_When_Not_Exists()
        {
            _movie.Genre = "nothing";
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.Genre)
                  .WithErrorMessage($"Genre {_movie.Genre} is not a valid value.");
        }
        
        [Fact]
        public void Can_Not_Validate_YearReleased_When_In_Future()
        {
            _movie.YearReleased = DateTime.UtcNow.Year + 1;
            
            var result = _validator.TestValidate(_movie);

            result.ShouldHaveValidationErrorFor(mov => mov.YearReleased)
                  .WithErrorMessage("Year of release may not be in the future.");
        }
    }
}