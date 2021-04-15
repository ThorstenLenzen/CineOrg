using System.Linq;
using GraphQL.Types;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Persistence.Database;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class SeatQueryType: ObjectGraphType<DomainSeat>
    {
        public SeatQueryType(CineOrgContext context)
        {
            Field(theatre => theatre.Id);
            Field("rowletter", theatre => theatre.RowLetter.ToString());
            Field(theatre => theatre.SeatNumber);
            Field<StringGraphType>(
               "category", 
               resolve:  ctx => context.Seats.Single(mov => mov.Id == ctx.Source.Id).Category.Key);
        }
    }
}