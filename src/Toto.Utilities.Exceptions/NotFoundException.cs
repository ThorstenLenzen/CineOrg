using System.Collections.Generic;

namespace Toto.Utilities.Exceptions
{
    public class NotFoundException : ErrorResultException
    {
        private const string Code = "NOT_FOUND";
        
        public NotFoundException(string message, IEnumerable<string>? errors = null) : base(Code, message, errors)
        { }
    }
}