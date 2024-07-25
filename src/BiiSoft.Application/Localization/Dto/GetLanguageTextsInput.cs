using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using BiiSoft.Dtos;

namespace BiiSoft.Localization
{
    public class GetLanguageTextsInput : PagedSortFilterInputDto, IShouldNormalize
    {
        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongName)]
        public string SourceName { get; set; }

        [StringLength(10)]
        public string BaseLanguageName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string TargetLanguageName { get; set; }

        public string TargetValueFilter { get; set; }

        public void Normalize()
        {
            if (TargetValueFilter.IsNullOrEmpty())
            {
                TargetValueFilter = "ALL";
            }
        }
    }
}