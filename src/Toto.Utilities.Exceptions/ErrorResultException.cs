using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Toto.Utilities.Exceptions
{
    public abstract class ErrorResultException : Exception
    {
        public virtual ErrorResult ErrorResult { get; }

        public ErrorResultException(string code, string message, IEnumerable<string>? errors = null) : base(message)
        {
            ErrorResult = ErrorResult.Create(code, message, errors);
        }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(ErrorResult);
        }
    }
}