using System;

namespace Toto.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static bool ToBoolean(this object item)
        {
            if (item is bool value)
            {
                return value;
            }
            
            throw new ArgumentException("Object could not be converted to boolean.");
        }
    }
}