using System.Linq;
using GraphQL.Types;
using Toto.CineOrg.DomainModel;
using Toto.CineOrg.Persistence.Database;

namespace Toto.CineOrg.GraphQLApi.GraphQL.Types
{
    public class MovieQueryType : ObjectGraphType<DomainMovie>
    {
        public MovieQueryType(CineOrgContext context)
        {
            Field(movie => movie.Id);
            Field(movie => movie.Title);
            Field(movie => movie.Description);
            Field(movie => movie.YearReleased);
            
            Field<StringGraphType>(
               "genre", 
               resolve:  ctx => context.Movies.Single(mov => mov.Id == ctx.Source.Id).Genre.Key);
        }
    }
}