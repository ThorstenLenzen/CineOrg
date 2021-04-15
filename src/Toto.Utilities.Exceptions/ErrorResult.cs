using System;
using System.Collections.Generic;

namespace Toto.Utilities.Exceptions
{
    public class ErrorResult
    {
        private readonly List<string> _errors;
        
        public string Code { get; }
        
        public string Message { get; }

        private IReadOnlyList<string> Errors => _errors;

        private ErrorResult(string code, string message, IEnumerable<string>? errors = null)
        {
            Code = code;
            Message = message;
            _errors = errors == null ? new List<string>() : new List<string>(errors);
        }

        public static ErrorResult Create(string code, string message, IEnumerable<string>? errors = null)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("Code must be provided.", nameof(code));
            
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message must be provided.", nameof(message));
            
            return new ErrorResult(code, message, errors);
        }
    }
}