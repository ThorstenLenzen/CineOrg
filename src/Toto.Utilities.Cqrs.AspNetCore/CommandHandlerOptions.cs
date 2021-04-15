using System;
using System.Linq.Expressions;

namespace Toto.Utilities.Cqrs.AspNetCore
{
    public class CommandHandlerOptions<TResult>
    {
        public string RouteName { get; set; }

        public Expression<Func<TResult, object>> IdProperty { get; set; }
        
        public Func<object, object>? ConversionFunction { get; set; }
    }
}