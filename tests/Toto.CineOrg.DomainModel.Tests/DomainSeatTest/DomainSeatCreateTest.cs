using System;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.DomainModel.Tests.DomainSeatTest
{
    public class DomainSeatCreateTest
    {
        private readonly char _validRowLetter;
        private readonly int _validSeatNumber;
        private readonly DomainSeatCategory _validSeatCategory;

        public DomainSeatCreateTest()
        {
            _validRowLetter = RandomGenerator.RandomLetter();
            _validSeatNumber = RandomGenerator.RandomNumber(1, 25);
            _validSeatCategory = DomainSeatCategory.Loge;
        }

        [Fact]
        public void Can_Create_Valid_DomainSeat()
        {
            var seat = DomainSeat.Create(_validSeatNumber, _validRowLetter, _validSeatCategory);

            seat.Id.Should().NotBe(Guid.Empty);
            seat.RowLetter.Should().Be(_validRowLetter);
            seat.SeatNumber.Should().Be(_validSeatNumber);
            seat.Category.Should().Be(_validSeatCategory);
        }
    }
}