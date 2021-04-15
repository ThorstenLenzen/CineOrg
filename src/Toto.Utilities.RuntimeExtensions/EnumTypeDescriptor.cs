using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    /// <summary>
    /// Type descriptor for <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    [NullableContext(1)]
    [Nullable(0)]
    public class EnumTypeDescriptor : CustomTypeDescriptor
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> _converterLookup = new ConcurrentDictionary<Type, TypeConverter>();
        private readonly Type _objectType;

        /// <summary>
        /// Initializes new instance of <see cref="T:Thinktecture.EnumTypeDescriptor" />.
        /// </summary>
        /// <param name="parent">Parent type descriptor.</param>
        /// <param name="objectType">Type of an enumeration.</param>
        public EnumTypeDescriptor(ICustomTypeDescriptor parent, Type objectType)
            : base(parent)
        {
            Type type = objectType;
            if ((object) type == null)
                throw new ArgumentNullException(nameof (objectType));
            this._objectType = type;
        }

        /// <inheritdoc />
        public override TypeConverter GetConverter()
        {
            return EnumTypeDescriptor.GetCachedConverter(this._objectType);
        }

        private static TypeConverter GetCachedConverter(Type type)
        {
            return EnumTypeDescriptor._converterLookup.GetOrAdd(type, new Func<Type, TypeConverter>(EnumTypeDescriptor.CreateTypeConverter));
        }

        private static TypeConverter CreateTypeConverter(Type type)
        {
            return (TypeConverter) Activator.CreateInstance(typeof (EnumTypeConverter<,>).MakeGenericType(EnumTypeDescriptor.GetEnumTypesArguments(type)));
        }

        private static Type[] GetEnumTypesArguments(Type type)
        {
            TypeInfo enumTypeDefinition = type.FindGenericEnumTypeDefinition();
            if ((Type) enumTypeDefinition != (Type) null)
                return enumTypeDefinition.GenericTypeArguments;
            throw new ArgumentException("The provided type " + type.FullName + " does not inherit the type Enum<,>");
        }
    }
}