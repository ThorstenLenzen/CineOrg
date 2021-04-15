using System;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainMovieTest
{
    public class DomainMovieSetReleaseYearTest
    {
        private readonly DomainMovie _movie;

        public DomainMovieSetReleaseYearTest()
        {
            var validTitle = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength);
            var validDescription = RandomGenerator.RandomString(DomainMovie.MaximumDescriptionLength);
            var validYearReleased = RandomGenerator.RandomPositiveNumber(2000);
            var genre = DomainGenre.Horror;

            _movie = DomainMovie.Create(validTitle, validDescription, genre, validYearReleased);
        }

        [Fact]
        public void Can_Set_Valid_DomainMovie_ReleaseYear()
        {
            var validYearReleased = RandomGenerator.RandomPositiveNumber(2000);
            
            _movie.SetYearReleased(validYearReleased);
            
            _movie.YearReleased.Should().Be(validYearReleased);
        }
        
        [Fact]
        public void Can_Not_Set_DomainMovie_With_Future_ReleaseYear()
        {
            var invalidYearReleased = DateTime.Now.Year + 2;
            
            Action action = () => _movie.SetYearReleased(invalidYearReleased);

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}