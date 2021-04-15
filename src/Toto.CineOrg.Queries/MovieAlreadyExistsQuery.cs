using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries
{
    public class MovieAlreadyExistsQuery : IQuery
    {
        public string Title { get; set; } = null!;
    }
}