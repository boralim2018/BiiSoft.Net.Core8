using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Extensions;
using BiiSoft.Authorization.Users;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.FieldBs.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;
using BiiSoft.Items;
using BiiSoft.Excels;

namespace BiiSoft.FieldBs
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class FieldBAppService : BiiSoftAppServiceBase, IFieldBAppService
    {
        private readonly IFieldBManager _fieldBManager;
        private readonly IBiiSoftRepository<FieldB, Guid> _fieldBRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        public FieldBAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IFieldBManager fieldBManager,
            IBiiSoftRepository<FieldB, Guid> fieldBRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _fieldBManager=fieldBManager;
            _fieldBRepository=fieldBRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _excelManager=excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_Create)]
        public async Task<Guid> Create(CreateUpdateFieldBInputDto input)
        {
            var entity = MapEntity<FieldB, Guid>(input);
            
            CheckErrors(await _fieldBManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _fieldBManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldBManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldBManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldBManager.SetAsDefaultAsync(entity));
        }
        
        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_SetAsDefault)]
        public async Task UnsetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldBManager.UnsetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_FieldBs)]
        public async Task<FindFieldBDto> GetDefaultValue()
        {
            var find = await _fieldBManager.GetDefaultValueAsync();
            return ObjectMapper.Map<FindFieldBDto>(find);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_FieldBs)]
        public async Task<PagedResultDto<FindFieldBDto>> Find(PageFieldBInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _fieldBRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
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
            var items = new List<FindFieldBDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FindFieldBDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Code = l.Code,
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

            return new PagedResultDto<FindFieldBDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_View, PermissionNames.Pages_Setup_Items_FieldBs_Edit)]
        public async Task<FieldBDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = _fieldBRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new FieldBDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            Code = l.Code,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _fieldBManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs)]
        public async Task<PagedResultDto<FieldBListDto>> GetList(PageFieldBInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<FieldBListDto>> GetListHelper(PageFieldBInputDto input)
        {   
            var query = _fieldBRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
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
            var items = new List<FieldBListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FieldBListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Code = l.Code,
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

            return new PagedResultDto<FieldBListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelFieldBInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<FieldBListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "FieldB.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _fieldBManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldBManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldBs_Edit)]
        public async Task Update(CreateUpdateFieldBInputDto input)
        {
            var entity = MapEntity<FieldB, Guid>(input);

            CheckErrors(await _fieldBManager.UpdateAsync(entity));
        }
    }
}
