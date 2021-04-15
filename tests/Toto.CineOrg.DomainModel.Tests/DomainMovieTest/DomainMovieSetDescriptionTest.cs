using System;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainMovieTest
{
    public class DomainMovieSetDescriptionTest
    {
        private readonly DomainMovie _movie;

        public DomainMovieSetDescriptionTest()
        {
            var validTitle = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength);
            var validDescription = RandomGenerator.RandomString(DomainMovie.MaximumDescriptionLength);
            var validYearReleased = RandomGenerator.RandomPositiveNumber(2000);
            var genre = DomainGenre.Horror;

            _movie = DomainMovie.Create(validTitle, validDescription, genre, validYearReleased);
        }

        [Fact]
        public void Can_Set_Valid_DomainMovie_Description()
        {
            var validDescription = RandomGenerator.RandomString(DomainMovie.MaximumDescriptionLength);
            
            _movie.SetDescription(validDescription);
            
            _movie.Description.Should().Be(validDescription);
        }
        
        [Fact]
        public void Can_Not_Set_DomainMovie_with_Null_Description()
        {
            Action action = () => _movie.SetDescription(null);;

            action.Should()
                .Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Can_Not_Set_DomainMovie_with_Long_Description()
        {
            var invalidDescription = RandomGenerator.RandomString(DomainMovie.MaximumDescriptionLength+10);
            
            Action action = () => _movie.SetDescription(invalidDescription);;

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}