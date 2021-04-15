using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Persistence.Database;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class TheatreQueryType: ObjectGraphType<DomainTheatre>
    {
        private readonly CineOrgContext _context;

        public TheatreQueryType(CineOrgContext context, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            _context = context;
            
            Field(theatre => theatre.Id);
            Field(theatre => theatre.Name);

            Field<ListGraphType<SeatQueryType>>(
                "seats", 
                resolve: ctx =>
                         {
                             // return context.Seats.Where(seat => seat.TheatreId == ctx.Source.Id).ToListAsync();

                             var loader = dataLoaderContextAccessor
                                .Context
                                .GetOrAddCollectionBatchLoader<Guid, DomainSeat>(
                                    "GetDomainSeatsByTheatreId",
                                    GetDomainSeatsByTheatreId);

                             return loader.LoadAsync(ctx.Source.Id);
                         });
        }

        public async Task<ILookup<Guid, DomainSeat>> GetDomainSeatsByTheatreId(IEnumerable<Guid> theatreIds)
        {
            var seats = await _context
                .Seats
                .Where(seat => theatreIds.Contains(seat.TheatreId))
                .ToListAsync();

            return seats.ToLookup(seat => seat.TheatreId);
        }
    }
}