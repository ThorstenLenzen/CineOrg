using System;
using System.Collections.Generic;
using System.Linq;
using Toto.CineOrg.DomainModel;

namespace Toto.CineOrg.ServiceModel.Converters
{
    public static class ServiceModelConverter
    {
        public static object ConvertDomainMovieToMovieDto(object domainMovie)
        {
            if (domainMovie is not DomainMovie typedMovie)
                throw new ArgumentException("domainMovie has to be of type DomainMovie.");
            
            return new Movie()
            {
                Id = typedMovie.Id,
                Title = typedMovie.Title,
                Description = typedMovie.Description,
                Genre = typedMovie.Genre.ToString(),
                YearReleased = typedMovie.YearReleased
            };
        }

        public static object ConvertDomainMovieListToMovieListDto(object domainMovieList)
        {
            if (domainMovieList is not IList<DomainMovie> typedMovieList)
                throw new ArgumentException("domainMovie has to be of type DomainMovie.");

            return typedMovieList
               .Select(ConvertDomainMovieToMovieDto)
               .ToList();
        }
    }
}