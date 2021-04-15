using System;
using System.Linq;
using System.Threading.Tasks;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.Persistence.Database
{
    public class QueryCineOrgContext : IDisposable
    {
        private readonly CineOrgContext _context;

        public QueryCineOrgContext(CineOrgContext context)
        {
            _context = context;
        }

        public IQueryable<DomainMovie> Movies => _context.Movies;
        
        public IQueryable<DomainTheatre> Theatres => _context.Theatres;
        
        public IQueryable<DomainSeat> Seats => _context.Seats;
        
        public async Task<TEntity> FindAsync<TEntity>(Guid id) where TEntity : class
            => await _context.FindAsync<TEntity>(id);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}