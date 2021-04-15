using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toto.Utilities.RuntimeExtensions;

namespace Toto.Utilities.EntityFrameworkCore.ValueConversion
{
    /// <summary>
    /// Value converter for <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    /// <typeparam name="TEnum">Type of the enum.</typeparam>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    [RuntimeExtensions.Nullable(new byte[] {0, 1, 1})]
    public class EnumValueConverter<TEnum, [RuntimeExtensions.Nullable(2)] TKey> : ValueConverter<TEnum, TKey>
        where TEnum : Enum<TEnum, TKey>
    {
        /// <summary>
        /// Initializes new instance <see cref="T:Thinktecture.EntityFrameworkCore.Storage.ValueConversion.EnumValueConverter`2" />.
        /// </summary>
        public EnumValueConverter()
            : base((Expression<Func<TEnum, TKey>>) (item => item.Key),
                   (Expression<Func<TKey, TEnum>>) (key => Enum<TEnum, TKey>.Get(key)), (ConverterMappingHints) null)
        {
        }
    }
}