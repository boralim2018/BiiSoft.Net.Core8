using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
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
using BiiSoft.Warehouses.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;

namespace BiiSoft.Warehouses
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class WarehouseAppService : BiiSoftAppServiceBase, IWarehouseAppService
    {
        private readonly IWarehouseManager _warehouseManager;
        private readonly IBiiSoftRepository<Warehouse, Guid> _warehouseRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        public WarehouseAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IWarehouseManager warehouseManager,
            IBiiSoftRepository<Warehouse, Guid> warehouseRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _warehouseManager=warehouseManager;
            _warehouseRepository=warehouseRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _excelManager=excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Create)]
        public async Task<Guid> Create(CreateUpdateWarehouseInputDto input)
        {
            var entity = MapEntity<Warehouse, Guid>(input);
            
            CheckErrors(await _warehouseManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _warehouseManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _warehouseManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _warehouseManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _warehouseManager.SetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Warehouses)]
        public async Task<FindWarehouseDto> GetDefaultValue()
        {
            var find = await _warehouseManager.GetDefaultValueAsync();
            return ObjectMapper.Map<FindWarehouseDto>(find);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Warehouses)]
        public async Task<PagedResultDto<FindWarehouseDto>> Find(FindWarehouseInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();
            var multiBranchEnable = await GetMultiBranchEnableAsync();
            var userBranchIds = await GetUserBranchIdsAsync();

            var query = _warehouseRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.BranchFilter != null && !input.BranchFilter.Ids.IsNullOrEmpty(), s => 
                            (input.BranchFilter.Exclude && !s.WarehouseBranches.Any(r => input.BranchFilter.Ids.Contains(r.BranchId))) ||
                            (!input.BranchFilter.Exclude && s.WarehouseBranches.Any(r => input.BranchFilter.Ids.Contains(r.BranchId))))
                        .WhereIf(multiBranchEnable, s => s.Sharing == BranchSharing.All || s.WarehouseBranches.Any(r => userBranchIds.Contains(r.BranchId)))
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<FindWarehouseDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindWarehouseDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Code = l.Code,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindWarehouseDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_View, PermissionNames.Pages_Setup_Warehouses_Edit)]
        public async Task<WarehouseDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = _warehouseRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new WarehouseDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            Code = l.Code,
                            Sharing = l.Sharing,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                            WarehouseBranches = l.WarehouseBranches.Select(s => new WarehouseBranchDto
                            {
                                Id = s.Id,
                                WarehouseId = s.WarehouseId,
                                BranchId = s.BranchId,
                                BranchName = s.Branch.Name,
                            }).ToList()
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _warehouseManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses)]
        public async Task<PagedResultDto<WarehouseListDto>> GetList(PageWarehouseInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<WarehouseListDto>> GetListHelper(PageWarehouseInputDto input)
        {
            var multiBranchEnable = await GetMultiBranchEnableAsync();
            var userBranchIds = await GetUserBranchIdsAsync();

            var query = _warehouseRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.BranchFilter != null && !input.BranchFilter.Ids.IsNullOrEmpty(), s =>
                            (input.BranchFilter.Exclude && !s.WarehouseBranches.Any(r => input.BranchFilter.Ids.Contains(r.BranchId))) ||
                            (!input.BranchFilter.Exclude && s.WarehouseBranches.Any(r => input.BranchFilter.Ids.Contains(r.BranchId))))
                        .WhereIf(multiBranchEnable, s => s.Sharing == BranchSharing.All || s.WarehouseBranches.Any(r => userBranchIds.Contains(r.BranchId)))
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<WarehouseListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new WarehouseListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Code = l.Code,
                    Sharing = l.Sharing,
                    IsDefault = l.IsDefault,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserId = l.LastModifierUserId,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<WarehouseListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelWarehouseInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<WarehouseListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "Warehouse.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _warehouseManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _warehouseManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Warehouses_Edit)]
        public async Task Update(CreateUpdateWarehouseInputDto input)
        {
            var entity = MapEntity<Warehouse, Guid>(input);

            CheckErrors(await _warehouseManager.UpdateAsync(entity));
        }
    }
}
