using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.Commands.Converters
{
    public static class DomainCreatingExtensions
    {
        public static DomainMovie ConvertToDomain(this CreateMovieCommand movie)
        {
            return DomainMovie.Create(
                                      movie.Title, 
                                      movie.Description, 
                                      DomainGenre.Get(movie.Genre),
                                      movie.YearReleased);
        }
        
        public static DomainMovie ConvertToDomain(this UpdateMovieCommand movie)
        {
            return DomainMovie.Create(
                                      movie.Id,
                                      movie.Title, 
                                      movie.Description, 
                                      DomainGenre.Get(movie.Genre),
                                      movie.YearReleased);
        }
        
        public static DomainMovie CopyValuesFrom(this DomainMovie copyTo, DomainMovie copyFrom)
        {
            copyTo.SetTitle(copyFrom.Title);
            copyTo.SetDescription(copyFrom.Description);
            copyTo.SetYearReleased(copyFrom.YearReleased);
            copyTo.SetGenre(copyFrom.Genre);
            
            return copyTo;
        }
    }
}