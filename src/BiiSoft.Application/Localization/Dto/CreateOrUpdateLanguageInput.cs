using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}