using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Toto.Utilities.EntityFrameworkCore
{
    public class UtcDateTimeValueConverter : ValueConverter<DateTime, DateTime>
    {
        public static readonly UtcDateTimeValueConverter Instance = new UtcDateTimeValueConverter();

        public UtcDateTimeValueConverter()
            : base(date => date.ToUniversalTime(), date => DateTime.SpecifyKind(date, DateTimeKind.Utc))
        {
        }
    }
}