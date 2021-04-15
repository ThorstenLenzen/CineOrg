using System;

namespace Toto.Utilities.Cqrs.Commands
{
    public class ProcessCommandException : Exception
    {
        public ProcessCommandException(string message) : base(message)
        { }
        
        public ProcessCommandException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}