using System;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainMovieTest
{
    public class DomainMovieSetTitleTest
    {
        private readonly DomainMovie _movie;

        public DomainMovieSetTitleTest()
        {
            var validTitle = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength);
            var validDescription = RandomGenerator.RandomString(DomainMovie.MaximumDescriptionLength);
            var validYearReleased = RandomGenerator.RandomPositiveNumber(2000);
            var genre = DomainGenre.Horror;

            _movie = DomainMovie.Create(validTitle, validDescription, genre, validYearReleased);
        }

        [Fact]
        public void Can_Set_Valid_DomainMovie_Title()
        {
            var validTitle = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength);
            
            _movie.SetTitle(validTitle);
            
            _movie.Title.Should().Be(validTitle);
        }
        
        [Fact]
        public void Can_Not_Set_DomainMovie_with_Null_Title()
        {
            Action action = () => _movie.SetTitle(null);;

            action.Should()
                .Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Can_Not_Set_DomainMovie_with_Long_Title()
        {
            var invalidTitle = RandomGenerator.RandomString(DomainMovie.MaximumTitleLength+10);
            
            Action action = () => _movie.SetTitle(invalidTitle);;

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}