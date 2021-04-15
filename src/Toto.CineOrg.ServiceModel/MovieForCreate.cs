namespace Toto.CineOrg.ServiceModel
{
    public class MovieForCreate
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        
        public string Genre { get; set; } = null!;
        
        public int YearReleased { get; set; }
    }
}