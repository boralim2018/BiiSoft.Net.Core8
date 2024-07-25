using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Dtos;
using BiiSoft.MultiTenancy.Dto;
using System.Threading.Tasks;

namespace BiiSoft.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantInputDto, CreateTenantDto, TenantDto>
    {
        Task<GetTenantFeaturesEditOutput> GetTenantFeaturesForEdit(EntityDto input);
        Task UpdateTenantFeatures(UpdateTenantFeaturesInput input);
        Task ResetTenantSpecificFeatures(EntityDto input);
        Task Enable(EntityDto input);
        Task Disable(EntityDto input);
    }
}

