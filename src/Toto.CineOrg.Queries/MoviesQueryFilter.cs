using System;

namespace Toto.CineOrg.Queries
{
    public class MoviesQueryFilter
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 100;
        public string OrderBy { get; set; } = string.Empty;

        public override string ToString()
        {
            var orderBy = string.IsNullOrEmpty(OrderBy) ? "<empty>" : OrderBy;
            return $"Skip:{Skip}; Take:{Take}; OrderBy:{orderBy}";
        }
    }
}