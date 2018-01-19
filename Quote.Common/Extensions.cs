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
                decimal result; if (decimal.TryParse(@this, out result)) return (T)(object)result; else return null;
            }

            //TODO: Add more types if needed
            
            try { return (T)Convert.ChangeType(@this, typeof(T)); }
            catch { return null; }
        }

        /// <summary>
        /// Checks whether a string is empty or not
        /// </summary>
        /// <returns>True if object is null or empty</returns>
        public static bool IsEmpty(this string @this) => string.IsNullOrEmpty(@this);
    }
}
