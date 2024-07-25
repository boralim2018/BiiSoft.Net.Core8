using System.ComponentModel.DataAnnotations;
using Abp.Localization;

namespace BiiSoft.Localization.Dto
{
    public class SetDefaultLanguageInput
    {
        [Required]
        [StringLength(10)]
        public virtual string Name { get; set; }
    }
}