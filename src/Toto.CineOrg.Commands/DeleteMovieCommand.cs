using System;
using Toto.Utilities.Cqrs.Commands;

namespace Toto.CineOrg.Commands
{
    public class DeleteMovieCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
