using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Security
{
    public class PasswordComplexitySetting
    {
        public bool Equals(PasswordComplexitySetting other)
        {
            if (other == null)
            {
                return false;
            }

            return
                RequireDigit == other.RequireDigit &&
                RequireLowercase == other.RequireLowercase &&
                RequireNonAlphanumeric == other.RequireNonAlphanumeric &&
                RequireUppercase == other.RequireUppercase &&
                RequiredLength == other.RequiredLength;
        }

        public string Pattern => 
            $"^{(RequireDigit ? "(?=.*\\d)" : "")}" +
            $"{(RequireLowercase ? "(?=.*[a-z])" : "")}" +
            $"{(RequireUppercase ? "(?=.*[A-Z])" : "")}" +
            $"{(RequireNonAlphanumeric ? "(?=.*\\W)" : "")}" +
            $"{".{"+RequiredLength+",}"}$";

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireUppercase { get; set; }

        public int RequiredLength { get; set; }
    }
}
