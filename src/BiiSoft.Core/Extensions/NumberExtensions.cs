using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Extensions
{
    public static class NumberExtensions
    {
        public static double PixcelToInches(this decimal pixels)
        {
            var result = (Convert.ToInt32(pixels) - 12) / 7d + 1;

            return result;
        }


        /// <summary>
        /// Generate string format start with prefix and follow by number with 0 pad left
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <returns>Ex: Prefix = X, Length = 5 return X0001</returns>
        public static string GenerateCode(this long index, int length, string prefix = "")
        {
            return $"{prefix}{index.ToString().PadLeft(length - prefix.Length, '0')}";
        }

        /// <summary>
        /// Generate string format start with prefix and follow by number with 0 pad left
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <returns>Ex: Prefix = X, Length = 5 return X0001</returns>
        public static string GenerateCode(this int index, int length, string prefix = "")
        {
            return $"{prefix}{index.ToString().PadLeft(length - prefix.Length, '0')}";
        }
    }
}
