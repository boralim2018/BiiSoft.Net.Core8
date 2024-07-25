using System.Threading.Tasks;
using Abp.Application.Services;
using BiiSoft.Configuration.Tenants.Dto;

namespace BiiSoft.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
