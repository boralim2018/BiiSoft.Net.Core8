using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Localization.Dto
{
    public class ApplicationLanguageEditDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        public string DisplayName { get; set; }

        [StringLength(BiiSoftConsts.MaxLengthLongName)]
        public string Icon { get; set; }

        /// <summary>
        /// Mapped from Language.IsDisabled with using manual mapping in CustomDtoMapper.cs
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}