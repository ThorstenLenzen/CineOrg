using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.Persistence.Database
{
    public class MovieConfiguration : IEntityTypeConfiguration<DomainMovie>
    {
        public void Configure(EntityTypeBuilder<DomainMovie> builder)
        {
            builder.ToTable("Movies");
            
            builder.Property(movie => movie.Title)
                   .HasMaxLength(100)
                   .IsRequired();
            
            builder.Property(movie => movie.Description)
                   .HasMaxLength(500)
                   .IsRequired();
            
            builder.Property(movie => movie.Genre).IsRequired();
            
            builder.HasIndex(movie => movie.Title);
            
            builder.HasData(new
            {
                Id = new Guid("20DB69D0-7760-4C3F-A484-032423E61018"),
                Title = "Sabrina",
                Description = "An ugly duckling having undergone a remarkable change, still harbors feelings for her crush: a carefree playboy, but not before his business-focused brother has something to say about it.",
                Genre = DomainGenre.Romance,
                YearReleased = 1995,
                CreatedAt = new DateTime(2020,4,3).ToUniversalTime()
            });
            
            builder.HasData(new
            {
                Id = new Guid("428429C0-9108-401C-B571-A09DC156AE50"),
                Title = "Patriot Games",
                Description = "When CIA Analyst Jack Ryan interferes with an IRA assassination, a renegade faction targets him and his family for revenge.",
                Genre = DomainGenre.Thriller,
                YearReleased = 1992,
                CreatedAt = new DateTime(2019,4,3).ToUniversalTime()
            });
        }
    }
}