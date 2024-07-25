using Abp.Application.Services.Dto;

namespace BiiSoft.Localization.Dto
{
    public class ApplicationLanguageListDto : FullAuditedEntityDto
    {
        public int? TenantId { get; set; }
        
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public bool IsDisabled { get; set; }
    }
}