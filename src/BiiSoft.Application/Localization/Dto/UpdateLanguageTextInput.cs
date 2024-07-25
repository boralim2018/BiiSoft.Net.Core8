using System.ComponentModel.DataAnnotations;
using Abp.Localization;

namespace BiiSoft.Localization.Dto
{
    public class UpdateLanguageTextInput
    {
        [Required]
        [StringLength(10)] //10
        public string LanguageName { get; set; }

        [Required]
        [StringLength(BiiSoftConsts.MaxLengthLongName)] //128
        public string SourceName { get; set; }

        [Required]
        [StringLength(BiiSoftConsts.MaxLengthDescription)] //256
        public string Key { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(67108864)] //67108864
        public string Value { get; set; }
    }
}