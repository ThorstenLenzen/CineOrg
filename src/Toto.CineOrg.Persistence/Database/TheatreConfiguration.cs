using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.Persistence.Database
{
    public class TheatreConfiguration : IEntityTypeConfiguration<DomainTheatre>
    {
        public void Configure(EntityTypeBuilder<DomainTheatre> builder)
        {
            builder.ToTable("Theatres");
            
            builder.Property(theatre => theatre.Name)
                   .HasMaxLength(100)
                   .IsRequired();
            
            builder.HasData(new
            {
                Id = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                Name = "Test Theatre - One"
            });
        }
    }
}