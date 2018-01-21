using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Common
{
    public static class Extensions
    {

        /// <summary>
        /// Tries to parse string to generic type you provide, returns null if cannot.
        /// </summary>
        public static T? TryParseAs<T>(this string @this) where T : struct
        {
            if (@this.IsEmpty()) return default(T?);

            if (typeof(T) == typeof(decimal))
            {
                if (decimal.TryParse(@this, out decimal result))
                    return (T)(object)result;
                else return null;
            }

            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(@this, out int result))
                    return (T)(object)result;
                else return null;
            }

            //TODO: Add more types if needed

            throw new NotImplementedException($"This extension method is not yet implemented for types {typeof(T)}.");
        }

        /// <summary>
        /// Checks whether a string is empty or not
        /// </summary>
        /// <returns>True if object is null or empty</returns>
        public static bool IsEmpty(this string @this) => string.IsNullOrEmpty(@this);

        /// <summary>
        /// Rounds the decimal value using 'Round half away from zero' technique.
        /// For example, 23.5 gets rounded to 24, and −23.5 gets rounded to −24.
        /// </summary>
        public static decimal RoundTo(this decimal value, int digits)
        {
            return decimal.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Will multiply the amount by 100 and them append percentage sign (%) at the end.
        /// Example: If .075 is passed, it will return 7.5%
        /// </summary>
        public static string DisplayPercentage(this decimal @this)
        {
            return $"{(@this * 100).RoundTo(1)} %";
        }
    }
}
