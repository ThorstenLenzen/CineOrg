using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    /// <summary>
    /// Non-generic interface implemented by <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    [NullableContext(1)]
    public interface IEnum
    {
        /// <summary>
        /// Indication whether the current enumeration item is valid or not.
        /// </summary>
        bool IsValid { get; }

        /// <summary>Checks whether current enumeration item is valid.</summary>
        /// <exception cref="T:System.InvalidOperationException">The enumeration item is not valid.</exception>
        void EnsureValid();

        /// <summary>The key of the enumeration item.</summary>
        object Key { get; }
    }
}