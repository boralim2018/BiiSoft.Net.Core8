using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace BiiSoft.Localization.Dto
{
    public class PagedLanguagesResultDto : PagedResultDto<ApplicationLanguageListDto>
    {
        public string DefaultLanguageName { get; set; }

        public PagedLanguagesResultDto()
        {
            
        }

        public PagedLanguagesResultDto(int totalCount, IReadOnlyList<ApplicationLanguageListDto> items, string defaultLanguageName)
            : base(totalCount, items)
        {
            DefaultLanguageName = defaultLanguageName;
        }
    }
}