using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Configuration.Dto;
using BiiSoft.Configuration.Host.Dto;
using BiiSoft.Dtos;

namespace BiiSoft.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);

        Task<ListResultDto<string>> GetTimeZones(PagedFilterInputDto input);


    }
}
