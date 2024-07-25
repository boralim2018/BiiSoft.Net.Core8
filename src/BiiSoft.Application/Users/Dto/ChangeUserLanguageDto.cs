using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}