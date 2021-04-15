using System;

namespace Toto.Utilities.Cqrs.Queries
{
    public class ProcessQueryException : Exception
    {
        public ProcessQueryException(string message) : base(message)
        { }
        
        public ProcessQueryException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}