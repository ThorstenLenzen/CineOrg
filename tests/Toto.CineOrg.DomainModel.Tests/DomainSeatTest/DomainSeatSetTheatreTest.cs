using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainSeatTest
{
    public class DomainSeatSetTheatreTest
    {
        private readonly DomainSeat _seat;
        private static DomainTheatre Theatre => DomainTheatre.Create(RandomGenerator.RandomString(35));

        public DomainSeatSetTheatreTest()
        {
            var validRowLetter = RandomGenerator.RandomLetter();
            var validSeatNumber = RandomGenerator.RandomNumber(1, 25);
            var validSeatCategory = DomainSeatCategory.Loge;
            
            _seat = DomainSeat.Create(validSeatNumber, validRowLetter, validSeatCategory);
        }

        [Fact]
        public void Can_Set_Valid_DomainTheatre()
        {
            var theatre = Theatre;
            
            _seat.SetTheatre(theatre);

            _seat.Theatre.Should().Be(theatre);
            _seat.TheatreId.Should().Be(theatre.Id);

            theatre.Seats.Should().Contain(_seat);
        }
        
        [Fact]
        public void Can_Not_Set_DomainTheatre_When_Seat_Already_Exists()
        {
            var theatre = Theatre;
            
            _seat.SetTheatre(theatre);
            
            theatre.Seats.Should()
                .ContainSingle(seat => seat.RowLetter == _seat.RowLetter && seat.SeatNumber == _seat.SeatNumber);
            
            _seat.SetTheatre(theatre); // Second time should not work!
            
            theatre.Seats.Should()
                .ContainSingle(seat => seat.RowLetter == _seat.RowLetter && seat.SeatNumber == _seat.SeatNumber);
        }
    }
}