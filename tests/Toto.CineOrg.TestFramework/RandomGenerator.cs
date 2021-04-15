using System;
using System.Text;

namespace Toto.CineOrg.TestFramework
{
    public static class RandomGenerator
    {
        private static Random _random = new();
        
        // Generate a random number between two numbers  
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static int RandomPositiveNumber(int max)
        {
            return RandomNumber(0, max);
        }

        // Generate a random string with a given size    
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        
        // Generate a random single char    
        public static char RandomLetter(bool lowerCase = false)
        {
            var letterCode = lowerCase ? _random.Next(97, 122) : _random.Next(65, 90);
            var letter = (char)letterCode;
            return letter;
        }

        // Generate a random password    
        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}