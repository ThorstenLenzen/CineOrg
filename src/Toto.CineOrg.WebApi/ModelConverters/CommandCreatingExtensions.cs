using System;
using Toto.CineOrg.Commands;
using Toto.CineOrg.ServiceModel;

namespace Toto.CineOrg.WebApi.ModelConverters
{
    public static class CommandCreatingExtensions
    {
        public static CreateMovieCommand ConvertToCommand(this MovieForCreate movie)
        {
            return new CreateMovieCommand
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                YearReleased = movie.YearReleased
            };
        }
        
        public static UpdateMovieCommand ConvertToCommand(this MovieForUpdate movie, Guid id)
        {
            return new UpdateMovieCommand
            {
                Id = id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                YearReleased = movie.YearReleased
            };
        }
    }
}