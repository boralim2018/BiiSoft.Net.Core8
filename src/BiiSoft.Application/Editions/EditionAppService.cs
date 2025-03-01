﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.Editions.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using OfficeOpenXml.Utils;
using BiiSoft.Features;

namespace BiiSoft.Editions
{
    [AbpAuthorize(PermissionNames.Pages_Editions, PermissionNames.Pages_Find_Editions)]
    public class EditionAppService : BiiSoftAppServiceBase, IEditionAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<Edition> _subscribableEditionRepository;

        public EditionAppService(
             EditionManager editionManager,
             IRepository<Edition> subscribableEditionRepository
            )
        {
            _editionManager = editionManager;
            _subscribableEditionRepository = subscribableEditionRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Editions, PermissionNames.Pages_Find_Editions)]
        public async Task<PagedResultDto<EditionListDto>> GetEditions(PagedEditionInputDto input)
        {

            var query = _subscribableEditionRepository.GetAll() 
                               .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), s =>
                                        s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                        s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                                .AsNoTracking()
                                .Select(s => new EditionListDto
                                {
                                    Id = s.Id,
                                    Name = s.Name,
                                    DisplayName = s.DisplayName
                                });

            var totalCount = await query.CountAsync();
            var items = new List<EditionListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<EditionListDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Editions_Create, PermissionNames.Pages_Editions_Edit)]
        public async Task<GetEditionEditOutput> GetEditionForEdit(NullableIdDto input)
        {
            var features = FeatureManager.GetAll()
                           .Where(f => f.Scope.HasFlag(FeatureScopes.Edition));

            EditionEditDto editionEditDto;
            List<NameValue> featureValues;

            if (input.Id.HasValue) //Editing existing edition?
            {
                var edition = await _editionManager.FindByIdAsync(input.Id.Value);
                featureValues = (await _editionManager.GetFeatureValuesAsync(input.Id.Value)).ToList();
                editionEditDto = ObjectMapper.Map<EditionEditDto>(edition);
            }
            else
            {
                editionEditDto = new EditionEditDto();
                featureValues = features.Select(f => new NameValue(f.Name, f.DefaultValue)).ToList();
            }

            var featureDtos = ObjectMapper.Map<List<FlatFeatureDto>>(features)
                              .OrderBy(f => f.ParentName)
                              .ThenBy(s => s.Name)
                              .ToList();
            var featureValueDtos = featureValues.Select(fv => new NameValueDto(fv)).ToList();

            return new GetEditionEditOutput
            {
                Edition = editionEditDto,
                Features = featureDtos,
                FeatureValues = featureValueDtos
            };
        }


        [AbpAuthorize(PermissionNames.Pages_Editions_Create, PermissionNames.Pages_Editions)]
        public async Task<ListResultDto<FlatFeatureDto>> GetFeatures()
        {
         
            var items = new List<FlatFeatureDto>();

            await Task.Run(() =>
            {
                var features = FeatureManager.GetAll()
                                .Where(f => f.Scope.HasFlag(FeatureScopes.Edition))
                                .ToList();
                items = ObjectMapper.Map<List<FlatFeatureDto>>(features)
                        .OrderBy(s => s.ParentName)
                        .ThenBy(s => s.Name)
                        .ToList();
            });

            return new ListResultDto<FlatFeatureDto> { Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Editions_Create, PermissionNames.Pages_Editions_Edit)]
        public async Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input)
        {
            if (!input.Edition.Id.HasValue)
            {
                await CreateEditionAsync(input);
            }
            else
            {
                await UpdateEditionAsync(input);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Editions_Delete)]
        public async Task DeleteEdition(EntityDto input)
        {
            var edition = await _editionManager.GetByIdAsync(input.Id);
            await _editionManager.DeleteAsync(edition);
        }

        public async Task<List<ComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null, bool addAllItem = false)
        {
            var editions = await _editionManager.Editions
                                 .Select(e => new ComboboxItemDto(e.Id.ToString(), e.DisplayName))
                                 .OrderBy(e => e.DisplayText).ToListAsync();


            var editionItems = new ListResultDto<ComboboxItemDto>(editions).Items.ToList();

            var defaultItem = new ComboboxItemDto("", L("NotAssigned"));
            editionItems.Insert(0, defaultItem);

            if (addAllItem)
            {
                editionItems.Insert(0, new ComboboxItemDto("-1", "- " + L("All") + " -"));
            }

            if (selectedEditionId.HasValue)
            {
                var selectedEdition = editionItems.FirstOrDefault(e => e.Value == selectedEditionId.Value.ToString());
                if (selectedEdition != null)
                {
                    selectedEdition.IsSelected = true;
                }
            }
            else
            {
                editionItems[0].IsSelected = true;
            }

            return editionItems;
        }

        protected virtual async Task CreateEditionAsync(CreateOrUpdateEditionDto input)
        {
            var edition = ObjectMapper.Map<Edition>(input.Edition);

            await _editionManager.CreateAsync(edition);
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the edition.

            await SetFeatureValues(edition, input.FeatureValues);
        }

        protected virtual async Task UpdateEditionAsync(CreateOrUpdateEditionDto input)
        {
            if (input.Edition.Id != null)
            {
                var edition = await _editionManager.GetByIdAsync(input.Edition.Id.Value);

                ObjectMapper.Map(input.Edition, edition);

                await SetFeatureValues(edition, input.FeatureValues);
            }
        }

        private Task SetFeatureValues(Edition edition, List<NameValueDto> featureValues)
        {
            return _editionManager.SetFeatureValuesAsync(edition.Id,
                featureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }
    }
}
