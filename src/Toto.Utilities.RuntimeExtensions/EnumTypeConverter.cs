using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    /// <summary>
  /// Type converter to convert an <see cref="T:Thinktecture.Enum`2" /> to <typeparamref name="TKey" /> and vice versa.
  /// </summary>
  /// <typeparam name="TEnum">Type of the concrete enumeration.</typeparam>
  /// <typeparam name="TKey">Type of the key.</typeparam>
  [NullableContext(1)]
  [Nullable(0)]
  public class EnumTypeConverter<[Nullable(0)] TEnum, [Nullable(2)] TKey> : TypeConverter
    where TEnum : Enum<TEnum, TKey>
  {
    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof (TKey) || sourceType == typeof (TEnum))
        return true;
      return typeof (TKey) != typeof (TEnum) ? TypeDescriptor.GetConverter(typeof (TKey)).CanConvertFrom(context, sourceType) : base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc />
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof (TKey) || destinationType == typeof (TEnum))
        return true;
      return typeof (TKey) != typeof (TEnum) ? TypeDescriptor.GetConverter(typeof (TKey)).CanConvertTo(context, destinationType) : base.CanConvertTo(context, destinationType);
    }

    /// <inheritdoc />
    [return: Nullable(2)]
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      [Nullable(2)] object value)
    {
      if (value == null)
        return (object) null;
      if (value is TKey key)
        return (object) Enum<TEnum, TKey>.Get(key);
      if (value is TEnum @enum)
        return (object) @enum;
      return typeof (TKey) != typeof (TEnum) ? (object) Enum<TEnum, TKey>.Get((TKey) TypeDescriptor.GetConverter(typeof (TKey)).ConvertFrom(context, culture, value)) : base.ConvertFrom(context, culture, value);
    }

    /// <inheritdoc />
    [return: Nullable(2)]
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      [Nullable(2)] object value,
      Type destinationType)
    {
      if (value == null)
        return !destinationType.GetTypeInfo().IsValueType ? (object) null : Activator.CreateInstance(destinationType);
      if (value is TEnum @enum)
      {
        if (destinationType == typeof (TKey))
          return (object) @enum.Key;
        if (destinationType == typeof (TEnum))
          return value;
        if (typeof (TKey) != typeof (TEnum))
          return TypeDescriptor.GetConverter(typeof (TKey)).ConvertTo(context, culture, (object) @enum.Key, destinationType);
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}