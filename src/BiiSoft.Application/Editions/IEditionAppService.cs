using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Dtos;
using BiiSoft.Editions.Dto;

namespace BiiSoft.Editions
{
    public interface IEditionAppService : IApplicationService
    {
        Task<PagedResultDto<EditionListDto>> GetEditions(PagedEditionInputDto inut);

        Task<GetEditionEditOutput> GetEditionForEdit(NullableIdDto input);

        Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input);

        Task DeleteEdition(EntityDto input);

        Task<ListResultDto<FlatFeatureDto>> GetFeatures();

        Task<List<SubscribableEditionComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null, bool addAllItem = false, bool onlyFree = false);
    }
}