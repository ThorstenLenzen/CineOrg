using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    /// <summary>
    /// Base class for enum-like classes using a <see cref="T:System.String" /> as a key.
    /// </summary>
    /// <remarks>
    /// Derived classes must have a default constructor for creation of "invalid" enumeration items.
    /// The default constructor should not be public.
    /// </remarks>
    /// <typeparam name="TEnum">Concrete type of the enumeration.</typeparam>
    [NullableContext(1)]
    [Nullable(new byte[] {0, 1, 1})]
    public abstract class Enum<[Nullable(0)] TEnum> : Enum<TEnum, string> where TEnum : Enum<TEnum, string>
    {
        static Enum()
        {
            Enum<TEnum, string>.KeyEqualityComparer = (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase;
        }

        /// <summary>
        /// Initializes new instance of <see cref="T:Thinktecture.Enum`2" />.
        /// </summary>
        /// <param name="key">The key of the enumeration item.</param>
        protected Enum(string key)
            : base(key)
        {
        }
    }
}