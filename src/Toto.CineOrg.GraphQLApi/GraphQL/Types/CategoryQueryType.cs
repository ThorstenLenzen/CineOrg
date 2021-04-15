using GraphQL.Types;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class CategoryQueryType : ObjectGraphType<DomainSeatCategory>
    {
        public CategoryQueryType()
        {
            Field("name", category => category.Key);
        }
    }
}