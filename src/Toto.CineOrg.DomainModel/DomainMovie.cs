using System;
using Toto.Utilities.Exceptions;
using Toto.Utilities.Extensions;

namespace Toto.CineOrg.DomainModel
{
    public class DomainMovie
    {
        public const int MaximumTitleLength = 100;
        
        public const int MaximumDescriptionLength = 450;
        
        public Guid Id { get; init; }

        public string Title { get; private set; }

        public string Description { get; private set; }
        
        public int YearReleased { get; private set; }
        
        public DomainGenre Genre { get; private set; }

        public DateTime CreatedAt { get; init; }

        # nullable disable
        private DomainMovie()
        {}
        #nullable restore

        public static DomainMovie Create(Guid id, string title, string description, DomainGenre genre, int yearReleased)
        {
            ValidateTitle(title);
            ValidateDescription(description);
            ValidateYearReleased(yearReleased);
            ValidateGenre(genre);

            var movie = new DomainMovie
            {
                Id = id,
                Title = title,
                Description = description,
                Genre = genre,
                YearReleased = yearReleased,
                CreatedAt = DateTime.UtcNow
            };

            return movie;
        }
        
        public static DomainMovie Create(string title, string description, DomainGenre genre, int yearReleased)
        {
            return Create(Guid.NewGuid(), title, description, genre, yearReleased);
        }

        public void SetDescription(string description)
        {
            ValidateDescription(description);
            
            Description = description;
        }
        
        public void SetTitle(string title)
        {
            ValidateTitle(title);
            
            Title = title;
        }
        
        public void SetYearReleased(int yearReleased)
        {
            ValidateYearReleased(yearReleased);
            
            YearReleased = yearReleased;
        }
        
        public void SetGenre(DomainGenre genre)
        {
            ValidateGenre(genre);
            
            Genre = genre;
        }
        
        private static void ValidateGenre(DomainGenre genre)
        {
            if (!genre.IsValid)
                throw new ArgumentNullException(nameof(genre));
        }

        private static void ValidateYearReleased(int yearReleased)
        {
            if (yearReleased > DateTime.UtcNow.Year)
                throw new ArgumentException("Release year must not be in the future", nameof(yearReleased));
        }

        private static void ValidateTitle(string title)
        {
            if (title.IsNullOrEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(title));
            
            if(title.Length > MaximumTitleLength)
                throw new ArgumentException($"Title exceeded maximum length of {MaximumTitleLength} characters.", nameof(title));
        }

        private static void ValidateDescription(string description)
        {
            if (description.IsNullOrEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(description));
            
            if(description.Length > MaximumDescriptionLength)
                throw new ArgumentException($"Description exceeded maximum length of {MaximumDescriptionLength} characters.", nameof(description));
        }
    }
}