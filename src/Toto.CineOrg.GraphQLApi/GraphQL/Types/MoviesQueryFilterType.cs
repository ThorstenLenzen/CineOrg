using GraphQL.Types;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class MoviesQueryFilterType : InputObjectGraphType
    {
        public MoviesQueryFilterType()
        {
            Name = "moviesQueryFilter";
            
            Field<IntGraphType>("skip");
            Field<IntGraphType>("take");
            Field<StringGraphType>("orderBy");
        }
    }
}