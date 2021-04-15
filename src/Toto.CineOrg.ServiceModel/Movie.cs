using System;

namespace Toto.CineOrg.ServiceModel
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        
        public string Genre { get; set; } = null!;
        
        public int YearReleased { get; set; }
    }
}