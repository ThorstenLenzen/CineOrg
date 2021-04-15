using System;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearReleased { get; set; }
        public string Genre { get; set; }
    }
}