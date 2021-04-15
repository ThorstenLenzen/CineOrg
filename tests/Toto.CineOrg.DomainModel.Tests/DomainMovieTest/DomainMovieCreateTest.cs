using System;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainMovieTest
{
    public class DomainMovieCreateTest
    {
        private readonly string _validTitle;
        private readonly string _invalidTitle;
        private readonly string _validDescription;
        private readonly string _invalidDescription;
        private readonly DomainGenre _genre;
        private readonly int _validYearReleased;
        private readonly int _invalidYearReleased;

        public DomainMovieCreateTest()
        {
            _validTitle = RandomGenerator.RandomString(DomainModel.DomainMovie.MaximumTitleLength);
            _invalidTitle = RandomGenerator.RandomString(DomainModel.DomainMovie.MaximumTitleLength+10);
            _validDescription = RandomGenerator.RandomString(DomainModel.DomainMovie.MaximumDescriptionLength);
            _invalidDescription = RandomGenerator.RandomString(DomainModel.DomainMovie.MaximumDescriptionLength+10);
            _genre = DomainGenre.Romance;
            _validYearReleased = RandomGenerator.RandomPositiveNumber(2000);
            _invalidYearReleased = DateTime.Now.Year+2;
        }

        [Fact]
        public void Can_Create_Valid_DomainMovie()
        {
            var movie = DomainMovie.Create(_validTitle, _validDescription, _genre, _validYearReleased);

            movie.Id.Should().NotBe(Guid.Empty);
            movie.Title.Should().Be(_validTitle);
            movie.Description.Should().Be(_validDescription);
            movie.Genre.Should().Be(_genre);
            movie.YearReleased.Should().Be(_validYearReleased);
            movie.CreatedAt.Should().BeBefore(DateTime.Now);
        }
        
        [Fact]
        public void Can_Not_Create_DomainMovie_With_Null_Title()
        {
            Action action = () => 
                DomainModel.DomainMovie.Create(null!, _validDescription, _genre, _validYearReleased);

            action.Should()
                .Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Can_Not_Create_DomainMovie_With_Long_Title()
        {
            Action action = () => 
                DomainModel.DomainMovie.Create(_invalidTitle, _validDescription, _genre, _validYearReleased);

            action.Should()
                .Throw<ArgumentException>();
        }
        
        [Fact]
        public void Can_Not_Create_DomainMovie_With_Null_Description()
        {
            Action action = () => 
                DomainModel.DomainMovie.Create(_validTitle, null!, _genre, _validYearReleased);

            action.Should()
                .Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Can_Not_Create_DomainMovie_With_Long_Description()
        {
            Action action = () => 
                DomainModel.DomainMovie.Create(_validTitle, _invalidDescription, _genre, _validYearReleased);

            action.Should()
                .Throw<ArgumentException>();
        }
        
        [Fact]
        public void Can_Not_Create_DomainMovie_With_Future_YearReleased()
        {
            Action action = () => 
                DomainModel.DomainMovie.Create(_validTitle, _invalidDescription, _genre, _invalidYearReleased);

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}