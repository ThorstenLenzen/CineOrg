using System;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes
{
    public class Seat
    {
        public Guid Id { get; set; }
        public string RowLetter { get; set; }
        public int SeatNumber { get; set; }
        public string Category { get; set; }
    }
}