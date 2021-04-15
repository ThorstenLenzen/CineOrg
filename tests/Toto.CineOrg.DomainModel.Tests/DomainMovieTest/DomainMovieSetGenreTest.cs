using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainMovieTest
{
    public class DomainMovieSetGenreTest
    {
        private readonly DomainMovie _movie;

        public DomainMovieSetGenreTest()
        {
            var validTitle = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength);
            var validDescription = RandomGenerator.RandomString(DomainMovie.MaximumDescriptionLength);
            var validYearReleased = RandomGenerator.RandomPositiveNumber(2000);
            var genre = DomainGenre.Horror;

            _movie = DomainMovie.Create(validTitle, validDescription, genre, validYearReleased);
        }

        [Fact]
        public void Can_Set_Valid_DomainMovie_Genre()
        {
            var genre = DomainGenre.Romance;
            
            _movie.SetGenre(genre);
            
            _movie.Genre.Should().Be(genre);
        }
    }
}