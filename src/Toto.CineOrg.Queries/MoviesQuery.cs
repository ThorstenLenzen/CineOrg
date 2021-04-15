using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries
{
    public class MoviesQuery : IQuery
    {
        public MoviesQuery()
        {
            Filter = new MoviesQueryFilter();
        }
        public MoviesQueryFilter Filter { get; set; }
    }
}