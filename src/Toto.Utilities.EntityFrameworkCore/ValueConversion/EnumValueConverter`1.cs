using Toto.Utilities.RuntimeExtensions;

namespace Toto.Utilities.EntityFrameworkCore.ValueConversion
{
    /// <summary>
    /// Value converter for <see cref="T:Thinktecture.Enum`1" />.
    /// </summary>
    /// <typeparam name="TEnum">Type of the enum.</typeparam>
    [Nullable(new byte[] {0, 1, 1})]
    public class EnumValueConverter<TEnum> : EnumValueConverter<TEnum, string> where TEnum : Enum<TEnum>
    {
    }
}