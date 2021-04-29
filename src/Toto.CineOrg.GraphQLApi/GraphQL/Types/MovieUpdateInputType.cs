using GraphQL.Types;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class MovieUpdateInputType : InputObjectGraphType
    {
        public MovieUpdateInputType()
        {
            Name = "movieUpdateInput";
            
            Field<NonNullGraphType<IdGraphType>>("id");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("description");
            Field<NonNullGraphType<IntGraphType>>("yearReleased");
            Field<NonNullGraphType<StringGraphType>>("genre");
        }
    }
}