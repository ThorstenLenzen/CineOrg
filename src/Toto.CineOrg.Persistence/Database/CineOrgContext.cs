using Microsoft.EntityFrameworkCore;
using Toto.CineOrg.DomainModel;
using Toto.Utilities.EntityFrameworkCore;

namespace Toto.CineOrg.Persistence.Database
{
    public class CineOrgContext : DbContext
    {
        public DbSet<DomainMovie> Movies { get; set; } = null!;
        
        public DbSet<DomainTheatre> Theatres { get; set; } = null!;
        
        public DbSet<DomainSeat> Seats { get; set; } = null!;
        
        public CineOrgContext(DbContextOptions<CineOrgContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.AddEnumConverter();
        }
    }
}