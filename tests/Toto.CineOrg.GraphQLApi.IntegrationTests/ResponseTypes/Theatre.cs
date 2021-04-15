using System;
using System.Collections.Generic;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes
{
    public class Theatre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Seat> Seats { get; set; }
    }
}