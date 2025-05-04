using System;

namespace BiiSoft.Extensions
{
    public static class NumberExtensions
    {
        public static double PixcelToInches(this decimal pixels)
        {
            var result = (Convert.ToInt32(pixels) - 12) / 7d + 1;

            return result;
        }

        public static bool IsNullOrZero(this long? id)
        {
            return !id.HasValue || id.Value == 0;
        }

        public static bool IsNullOrEmpty(this Guid? id)
        {
            return !id.HasValue || id.Value == Guid.Empty;
        }

    }
}
