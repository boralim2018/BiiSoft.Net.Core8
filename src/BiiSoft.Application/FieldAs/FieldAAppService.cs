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
using BiiSoft.FieldAs.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;
using BiiSoft.Items;
using BiiSoft.Excels;

namespace BiiSoft.FieldAs
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class FieldAAppService : BiiSoftAppServiceBase, IFieldAAppService
    {
        private readonly IFieldAManager _fieldAManager;
        private readonly IBiiSoftRepository<FieldA, Guid> _fieldARepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        public FieldAAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IFieldAManager fieldAManager,
            IBiiSoftRepository<FieldA, Guid> fieldARepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _fieldAManager=fieldAManager;
            _fieldARepository=fieldARepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _excelManager=excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_Create)]
        public async Task<Guid> Create(CreateUpdateFieldAInputDto input)
        {
            var entity = MapEntity<FieldA, Guid>(input);
            
            CheckErrors(await _fieldAManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _fieldAManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldAManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldAManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldAManager.SetAsDefaultAsync(entity));
        }
        
        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_SetAsDefault)]
        public async Task UnsetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldAManager.UnsetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_FieldAs)]
        public async Task<FindFieldADto> GetDefaultValue()
        {
            var find = await _fieldAManager.GetDefaultValueAsync();
            return ObjectMapper.Map<FindFieldADto>(find);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_FieldAs)]
        public async Task<PagedResultDto<FindFieldADto>> Find(PageFieldAInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _fieldARepository.GetAll()
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
            var items = new List<FindFieldADto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FindFieldADto
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

            return new PagedResultDto<FindFieldADto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_View, PermissionNames.Pages_Setup_Items_FieldAs_Edit)]
        public async Task<FieldADetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = _fieldARepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new FieldADetailDto
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

            await _fieldAManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs)]
        public async Task<PagedResultDto<FieldAListDto>> GetList(PageFieldAInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<FieldAListDto>> GetListHelper(PageFieldAInputDto input)
        {   
            var query = _fieldARepository.GetAll()
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
            var items = new List<FieldAListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FieldAListDto
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

            return new PagedResultDto<FieldAListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelFieldAInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<FieldAListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "FieldA.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _fieldAManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _fieldAManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_FieldAs_Edit)]
        public async Task Update(CreateUpdateFieldAInputDto input)
        {
            var entity = MapEntity<FieldA, Guid>(input);

            CheckErrors(await _fieldAManager.UpdateAsync(entity));
        }
    }
}
