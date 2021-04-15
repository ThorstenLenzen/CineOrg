using Toto.Utilities.RuntimeExtensions;

namespace Toto.CineOrg.DomainModel
{
    public class DomainSeatCategory : Enum<DomainSeatCategory>
    {
        public static readonly DomainSeatCategory Loge = new DomainSeatCategory("loge");
        public static readonly DomainSeatCategory Stalls = new DomainSeatCategory("stalls");

        private DomainSeatCategory(string key) : base(key)
        { }

        /// <inheritdoc />
        protected override DomainSeatCategory CreateInvalid(string key)
        {
            return new DomainSeatCategory(key);
        }
    }
}