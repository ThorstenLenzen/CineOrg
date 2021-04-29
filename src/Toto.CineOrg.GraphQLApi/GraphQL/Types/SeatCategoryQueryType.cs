using GraphQL.Types;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class SeatCategoryQueryType : ObjectGraphType<string>
    {
        public SeatCategoryQueryType()
        {
            Field("name", seat => seat);
        }
    }
}