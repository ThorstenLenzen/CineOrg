using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Type" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Searches for the (base) type implementing <see cref="T:Thinktecture.Enum`2" />
        /// </summary>
        /// <param name="type">Type to search in.</param>
        /// <returns>A generic type created from generic <see cref="T:Thinktecture.Enum`2" /> or <c>null</c> if the <paramref name="type" /> is not a generic enum.</returns>
        /// <exception cref="T:System.ArgumentNullException">Type is <c>null</c>.</exception>
        [NullableContext(1)]
        [return: Nullable(2)]
        public static TypeInfo FindGenericEnumTypeDefinition(this Type type)
        {
            if (type == (Type) null)
                throw new ArgumentNullException(nameof (type));
            TypeInfo typeInfo;
            for (; type != (Type) null && type != typeof (object); type = typeInfo.BaseType)
            {
                typeInfo = type.GetTypeInfo();
                if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof (Enum<,>))
                    return typeInfo;
            }
            return (TypeInfo) null;
        }
    }
}