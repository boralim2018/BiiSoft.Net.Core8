using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;
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
using BiiSoft.BOMs.Dto;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BiiSoft.BOMs
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class BOMAppService : BiiSoftAppServiceBase, IBOMAppService
    {
        private readonly IBOMManager _bomManager;
        private readonly IBiiSoftRepository<BOM, Guid> _bomRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        public BOMAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBOMManager bomManager,
            IBiiSoftRepository<BOM, Guid> bomRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _bomManager=bomManager;
            _bomRepository=bomRepository;
            _userRepository=userRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _excelManager = excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_Create)]
        public async Task<Guid> Create(CreateUpdateBOMInputDto input)
        {
            var entity = MapEntity<BOM, Guid>(input);
            
            CheckErrors(await _bomManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _bomManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _bomManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _bomManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _bomManager.SetAsDefaultAsync(entity));
        }
        
        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_SetAsDefault)]
        public async Task UnsetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _bomManager.UnsetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_BOMs)]
        public async Task<FindBOMDto> GetDefaultValue()
        {
            var find = await _bomManager.GetDefaultValueAsync();
            return ObjectMapper.Map<FindBOMDto>(find);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_BOMs)]
        public async Task<PagedResultDto<FindBOMDto>> Find(FindBOMInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();
          
            var query = _bomRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.ItemFilter != null && !input.ItemFilter.Ids.IsNullOrEmpty(), s => 
                            (input.ItemFilter.Exclude && !(input.ItemFilter.Ids.Contains(s.ItemId) || (s.Type == BOMType.StandardPackaging && s.BOMItems.Any(r => input.ItemFilter.Ids.Contains(r.ItemId))))) ||
                            (!input.ItemFilter.Exclude && (input.ItemFilter.Ids.Contains(s.ItemId) || (s.Type == BOMType.StandardPackaging && s.BOMItems.Any(r => input.ItemFilter.Ids.Contains(r.ItemId))))))
                        .WhereIf(input.TypeFilter != null && !input.TypeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.TypeFilter.Exclude && !input.TypeFilter.Ids.Contains(s.Type)) ||
                            (!input.TypeFilter.Exclude && input.TypeFilter.Ids.Contains(s.Type)))
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
            var items = new List<FindBOMDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FindBOMDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Type = l.Type,
                    TypeName = l.Type.GetName(),
                    ItemId = l.ItemId,
                    ItemName = isDefaultLanguage ? l.Item.Name : l.Item.DisplayName,
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

            return new PagedResultDto<FindBOMDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_View, PermissionNames.Pages_Setup_Items_BOMs_Edit)]
        public async Task<BOMDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _bomRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new BOMDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            Type = l.Type,
                            TypeName = l.Type.GetName(),
                            ItemId = l.ItemId,
                            ItemName = isDefaultLanguage ? l.Item.Name : l.Item.DisplayName,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                            BOMItems = l.BOMItems.Select(s => new BOMItemDto
                            {
                                Id = s.Id,
                                ItemId = s.ItemId,
                                ItemName = isDefaultLanguage ? s.Item.Name : s.Item.DisplayName,
                                Qty = s.Qty,
                            }).ToList()
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _bomManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs)]
        public async Task<PagedResultDto<BOMListDto>> GetList(PageBOMInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<BOMListDto>> GetListHelper(PageBOMInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _bomRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                           .WhereIf(input.ItemFilter != null && !input.ItemFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemFilter.Exclude && !(input.ItemFilter.Ids.Contains(s.ItemId) || (s.Type == BOMType.StandardPackaging && s.BOMItems.Any(r => input.ItemFilter.Ids.Contains(r.ItemId))))) ||
                            (!input.ItemFilter.Exclude && (input.ItemFilter.Ids.Contains(s.ItemId) || (s.Type == BOMType.StandardPackaging && s.BOMItems.Any(r => input.ItemFilter.Ids.Contains(r.ItemId))))))
                        .WhereIf(input.TypeFilter != null && !input.TypeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.TypeFilter.Exclude && !input.TypeFilter.Ids.Contains(s.Type)) ||
                            (!input.TypeFilter.Exclude && input.TypeFilter.Ids.Contains(s.Type)))
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
            var items = new List<BOMListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new BOMListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Type = l.Type,
                    TypeName = l.Type.GetName(),
                    ItemId = l.ItemId,
                    ItemName = isDefaultLanguage ? l.Item.Name : l.Item.DisplayName,
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

            return new PagedResultDto<BOMListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelBOMInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<BOMListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "BOM.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _bomManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _bomManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_BOMs_Edit)]
        public async Task Update(CreateUpdateBOMInputDto input)
        {
            var entity = MapEntity<BOM, Guid>(input);

            CheckErrors(await _bomManager.UpdateAsync(entity));
        }
    }
}
