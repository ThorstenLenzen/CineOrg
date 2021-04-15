using Toto.Utilities.RuntimeExtensions;

namespace Toto.CineOrg.DomainModel
{
    public class DomainGenre : Enum<DomainGenre>
    {
        public static readonly DomainGenre Thriller = new DomainGenre("thriller");
        public static readonly DomainGenre Romance = new DomainGenre("romance");
        public static readonly DomainGenre SciFi = new DomainGenre("scifi");
        public static readonly DomainGenre Fantasy = new DomainGenre("fantasy");
        public static readonly DomainGenre Horror = new DomainGenre("horror");
        public static readonly DomainGenre Comedy = new DomainGenre("comedy");
        public static readonly DomainGenre War = new DomainGenre("war");
        
        private DomainGenre(string key) : base(key)
        { }

        /// <inheritdoc />
        protected override DomainGenre CreateInvalid(string key)
        {
            return new DomainGenre(key);
        }
    }
}