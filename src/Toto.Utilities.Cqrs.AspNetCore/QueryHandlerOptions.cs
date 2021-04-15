using System;

namespace Toto.Utilities.Cqrs.AspNetCore
{
    public class QueryHandlerOptions
    {
        public Func<object, object>? ConversionFunction { get; set; }
    }
}