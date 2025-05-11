using System;

namespace BiiSoft.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum enumValue)
        {
            // Get the name of the enum (e.g. "CashOnHand")
            var enumName = enumValue.ToString();

            // Replace underscores with spaces and insert space before capital letters (Pascal Case -> Sentence Case)
            var formattedName = System.Text.RegularExpressions.Regex.Replace(enumName, @"([a-z])([A-Z])", "$1 $2");
            return formattedName.Replace("_", " "); // Replace underscores with spaces
        }
    }
}
