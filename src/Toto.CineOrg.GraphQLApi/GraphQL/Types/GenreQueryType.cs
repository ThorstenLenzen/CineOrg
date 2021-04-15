using GraphQL.Types;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class GenreQueryType : ObjectGraphType<string>
    {
        public GenreQueryType()
        {
            Field("name", genre => genre);
        }
    }
}