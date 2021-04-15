using System.Collections.Generic;

namespace Toto.Utilities.Exceptions
{
    public class InvalidParameterException : ErrorResultException
    {
        private const string Code = "BAD_REQUEST";
        
        public InvalidParameterException(string message, IEnumerable<string>? errors = null) : base(Code, message, errors)
        { }
    }
}