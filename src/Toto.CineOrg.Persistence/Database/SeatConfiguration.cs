using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.Persistence.Database
{
    public class SeatConfiguration : IEntityTypeConfiguration<DomainSeat>
    {
        public void Configure(EntityTypeBuilder<DomainSeat> builder)
        {
            builder.ToTable("Seats");
            
            builder.Property(seat => seat.Category).IsRequired();
            
            // builder.HasIndex(seat => seat.RowLetter)
            //        .IncludeProperties(seat => seat.SeatNumber)
            //        .IsUnique();

            builder.HasOne(seat => seat.Theatre)
                   .WithMany(theatre => theatre.Seats)
                   .HasForeignKey(seat => seat.TheatreId);
            
            builder.HasData(new
            {
                Id = new Guid("B409157A-5D8C-4545-AAD5-7D0F02B5E515"),
                TheatreId = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                RowLetter = 'A',
                SeatNumber = 1,
                Category = DomainSeatCategory.Stalls,
            });
            
            builder.HasData(new
            {
                Id = new Guid("B5111D43-7B41-45E3-A3B8-8F041275829B"),
                TheatreId = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                RowLetter = 'A',
                SeatNumber = 2,
                Category = DomainSeatCategory.Stalls,
            });
            
            builder.HasData(new
            {
                Id = new Guid("0306C201-81D1-441D-BD5B-54DBD35FA3EE"),
                TheatreId = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                RowLetter = 'A',
                SeatNumber = 3,
                Category = DomainSeatCategory.Stalls,
            });
            
            builder.HasData(new
            {
                Id = new Guid("4FF65E8F-C8A8-44AF-B7B3-C9B17852205D"),
                TheatreId = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                RowLetter = 'B',
                SeatNumber = 1,
                Category = DomainSeatCategory.Loge,
            });
            
            builder.HasData(new
            {
                Id = new Guid("24F905EA-7E41-4E3C-B2C6-83C08B9AE3A2"),
                TheatreId = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                RowLetter = 'B',
                SeatNumber = 2,
                Category = DomainSeatCategory.Loge,
            });
            
            builder.HasData(new
            {
                Id = new Guid("B160874D-5E67-410B-A6B6-14525C10467A"),
                TheatreId = new Guid("7973DADD-C7F8-42E5-83B9-729A8FF7C614"),
                RowLetter = 'B',
                SeatNumber = 3,
                Category = DomainSeatCategory.Loge,
            });
        }
    }
}