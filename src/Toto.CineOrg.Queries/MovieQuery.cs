using System;
using Toto.Utilities.Cqrs.Queries;

namespace Toto.CineOrg.Queries
{
    public class MovieQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}