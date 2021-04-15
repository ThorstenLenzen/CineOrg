using System;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.CineOrg.Commands
{
    public class UpdateMovieCommand : ICommand
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        
        public string Genre { get; set; } = null!;
        
        public int YearReleased { get; set; }
    }
}