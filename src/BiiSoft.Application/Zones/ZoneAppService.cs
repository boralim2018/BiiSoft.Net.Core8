using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Users;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using BiiSoft.Warehouses;
using BiiSoft.Zones.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;

namespace BiiSoft.Zones
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class ZoneAppService : BiiSoftAppServiceBase, IZoneAppService
    {
        private readonly IZoneManager _zoneManager;
        private readonly IBiiSoftRepository<Zone, Guid> _zoneRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        public ZoneAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IZoneManager zoneManager,
            IBiiSoftRepository<Zone, Guid> zoneRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _zoneManager=zoneManager;
            _zoneRepository=zoneRepository;
            _userRepository=userRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _excelManager = excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_Create)]
        public async Task<Guid> Create(CreateUpdateZoneInputDto input)
        {
            var entity = MapEntity<Zone, Guid>(input);
            
            CheckErrors(await _zoneManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _zoneManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _zoneManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _zoneManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _zoneManager.SetAsDefaultAsync(entity));
        }
        
        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_SetAsDefault)]
        public async Task UnsetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _zoneManager.UnsetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Zones)]
        public async Task<FindZoneDto> GetDefaultValue(EntityDto<Guid> input)
        {
            var find = await _zoneManager.GetDefaultValueAsync(input.Id);
            return ObjectMapper.Map<FindZoneDto>(find);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Zones)]
        public async Task<PagedResultDto<FindZoneDto>> Find(FindZoneInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();
            var multiBranchEnable = await GetMultiBranchEnableAsync();
            var userBranchIds = await GetUserBranchIdsAsync();

            var query = _zoneRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.WarehouseFilter != null && !input.WarehouseFilter.Ids.IsNullOrEmpty(), s => 
                            (input.WarehouseFilter.Exclude && !input.WarehouseFilter.Ids.Contains(s.WarehouseId)) ||
                            (!input.WarehouseFilter.Exclude && input.WarehouseFilter.Ids.Contains(s.WarehouseId)))
                        .WhereIf(multiBranchEnable, s => s.Warehouse.Sharing == BranchSharing.All || s.Warehouse.WarehouseBranches.Any(r => userBranchIds.Contains(r.BranchId)))
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<FindZoneDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FindZoneDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    WarehouseName = isDefaultLanguage ? l.Warehouse.Name : l.Warehouse.DisplayName,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<FindZoneDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_View, PermissionNames.Pages_Setup_Warehouses_Zones_Edit)]
        public async Task<ZoneDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _zoneRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new ZoneDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            WarehouseId = l.WarehouseId,
                            WarehouseName = isDefaultLanguage ? l.Warehouse.Name : l.Warehouse.DisplayName,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : ""
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _zoneManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones)]
        public async Task<PagedResultDto<ZoneListDto>> GetList(PageZoneInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<ZoneListDto>> GetListHelper(PageZoneInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();
            var multiBranchEnable = await GetMultiBranchEnableAsync();
            var userBranchIds = await GetUserBranchIdsAsync();

            var query = _zoneRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.WarehouseFilter != null && !input.WarehouseFilter.Ids.IsNullOrEmpty(), s =>
                            (input.WarehouseFilter.Exclude && !input.WarehouseFilter.Ids.Contains(s.WarehouseId)) ||
                            (!input.WarehouseFilter.Exclude && input.WarehouseFilter.Ids.Contains(s.WarehouseId)))
                        .WhereIf(multiBranchEnable, s => s.Warehouse.Sharing == BranchSharing.All || s.Warehouse.WarehouseBranches.Any(r => userBranchIds.Contains(r.BranchId)))
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<ZoneListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new ZoneListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    WarehouseName = isDefaultLanguage ? l.Warehouse.Name : l.Warehouse.DisplayName,
                    IsDefault = l.IsDefault,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserId = l.LastModifierUserId,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<ZoneListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelZoneInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<ZoneListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "Zone.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _zoneManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _zoneManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Zones_Edit)]
        public async Task Update(CreateUpdateZoneInputDto input)
        {
            var entity = MapEntity<Zone, Guid>(input);

            CheckErrors(await _zoneManager.UpdateAsync(entity));
        }
    }
}
