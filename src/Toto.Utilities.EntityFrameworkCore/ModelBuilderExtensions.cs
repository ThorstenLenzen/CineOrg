using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toto.Utilities.EntityFrameworkCore.ValueConversion;
using Toto.Utilities.RuntimeExtensions;

namespace Toto.Utilities.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        private static readonly MethodInfo _getKeyMaxLength = typeof(ModelBuilderExtensions).GetMethod(nameof(GetKeyMaxLength), BindingFlags.Static | BindingFlags.NonPublic);
        
        public static void AddEnumConverter(this ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties().Concat<IPropertyBase>(entityType.GetNavigations().ToList())) // copy properties over to new list because a navigation may be converted to scalar property
                {
                    if (typeof(IEnum).IsAssignableFrom(property.ClrType))
                    {
                        var converterType = typeof(EnumValueConverter<>).MakeGenericType(property.ClrType);
                        var converter = (ValueConverter)Activator.CreateInstance(converterType);

                        var propertyBuilder = modelBuilder.Entity(entityType.Name)
                                                          .Property(property.Name)
                                                          .HasConversion(converter)
                                                          .IsRequired();

                        var keyType = property.ClrType.FindGenericEnumTypeDefinition()?.GetGenericArguments()[1];

                        if (keyType == typeof(string))
                        {
                            var maxLength = (int)_getKeyMaxLength.MakeGenericMethod(property.ClrType).Invoke(null, Array.Empty<object>());
                            propertyBuilder.HasMaxLength(maxLength);
                        }
                    }
                }
            }
        }

        public static void AddUtcDateTimeConverter(this ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(UtcDateTimeValueConverter.Instance);
                    }
                }
            }
        }
        
        private static int GetKeyMaxLength<T>()
            where T : Enum<T, string>
        {
            var length = Enum<T, string>.GetAll().Max(i => i.Key.Length);

            // round up
            return length * 2;
        }
    }
}