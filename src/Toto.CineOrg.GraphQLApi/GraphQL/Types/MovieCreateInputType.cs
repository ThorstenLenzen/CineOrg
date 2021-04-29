using GraphQL.Types;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class MovieCreateInputType : InputObjectGraphType
    {
        public MovieCreateInputType()
        {
            Name = "movieCreateInput";
            
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("description");
            Field<NonNullGraphType<IntGraphType>>("yearReleased");
            Field<NonNullGraphType<StringGraphType>>("genre");
        }
    }
}