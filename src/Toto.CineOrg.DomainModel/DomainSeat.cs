using System;

namespace Toto.CineOrg.DomainModel
{
    public class DomainSeat
    {
        public Guid Id { get; init; }

        public int SeatNumber { get; init; }

        public char RowLetter { get; init; }

        public DomainSeatCategory Category { get; init; }
        
        public Guid TheatreId { get; internal set; }
        
        public DomainTheatre Theatre { get; internal set; }
        
        # nullable disable
        private DomainSeat()
        {}
        #nullable restore
        
        public static DomainSeat Create(int seatNumber, char rowLetter, DomainSeatCategory seatCategory)
        {
            var seat = new DomainSeat
            {
                Id = Guid.NewGuid(),
                SeatNumber = seatNumber,
                RowLetter = rowLetter,
                Category = seatCategory,
            };
            
            return seat;
        }

        public void SetTheatre(DomainTheatre theatre)
        {
            theatre.AddSeat(this);
        }
        
    }
}