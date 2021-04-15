using GraphQL.Types;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class MovieInputType : InputObjectGraphType
    {
        public MovieInputType()
        {
            Name = "movieInput";
            
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("description");
            Field<NonNullGraphType<IntGraphType>>("yearReleased");
            Field<NonNullGraphType<StringGraphType>>("genre");
        }
    }
}