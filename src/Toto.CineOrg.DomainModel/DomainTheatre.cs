using System;
using System.Collections.Generic;
using System.Linq;

namespace Toto.CineOrg.DomainModel
{
    public class DomainTheatre
    {
        private readonly List<DomainSeat> _seats;
        public Guid Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyList<DomainSeat> Seats => _seats;

        #nullable disable
        private DomainTheatre()
        {
            _seats = new List<DomainSeat>();
        }
        #nullable restore

        public static DomainTheatre Create(string name)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = name,
            };
        }

        public void AddSeat(char rowLetter, int seatNumber, DomainSeatCategory seatCategory)
        {
            var newSeat = DomainSeat.Create(seatNumber, rowLetter, seatCategory);
            AddSeat(newSeat);
        }

        public void AddSeat(DomainSeat seat)
        {
            var alreadyExists = _seats
                .Any(s => s.RowLetter == seat.RowLetter && s.SeatNumber == seat.SeatNumber);

            if (alreadyExists) return;

            seat.Theatre = this;
            seat.TheatreId = Id;
            
            _seats.Add(seat);
        }
    }
}