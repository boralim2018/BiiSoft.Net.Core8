using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.ChartOfAccounts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Extensions;
using BiiSoft.Authorization.Users;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;
using Abp.Collections.Extensions;
using BiiSoft.Enums;

namespace BiiSoft.ChartOfAccounts
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class ChartOfAccountAppService : BiiSoftExcelAppServiceBase, IChartOfAccountAppService
    {
        private readonly IChartOfAccountManager _chartOfAccountManager;
        private readonly IBiiSoftRepository<ChartOfAccount, Guid> _chartOfAccountRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ChartOfAccountAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IChartOfAccountManager chartOfAccountManager,
            IBiiSoftRepository<ChartOfAccount, Guid> chartOfAccountRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _chartOfAccountManager=chartOfAccountManager;
            _chartOfAccountRepository=chartOfAccountRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_Create)]
        public async Task<Guid> Create(CreateUpdateChartOfAccountInputDto input)
        {
            var entity = MapEntity<ChartOfAccount, Guid>(input);

            CheckErrors(await _chartOfAccountManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _chartOfAccountManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _chartOfAccountManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _chartOfAccountManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_ChartOfAccounts)]
        public async Task<PagedResultDto<FindChartOfAccountDto>> Find(FindChartOfAccountInputDto input)
        {
            var query = _chartOfAccountRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.AccountTypes != null && !input.AccountTypes.Ids.IsNullOrEmpty(), s =>
                            (input.AccountTypes.Exclude && (!input.AccountTypes.Ids.Contains(s.AccountType))) ||
                            (!input.AccountTypes.Exclude && input.AccountTypes.Ids.Contains(s.AccountType)))
                        .WhereIf(input.SubAccountTypes != null && !input.SubAccountTypes.Ids.IsNullOrEmpty(), s =>
                            (input.SubAccountTypes.Exclude && (!input.SubAccountTypes.Ids.Contains(s.SubAccountType))) ||
                            (!input.SubAccountTypes.Exclude && input.SubAccountTypes.Ids.Contains(s.SubAccountType)))
                        .WhereIf(input.Parents != null && input.Parents.Ids != null && input.Parents.Ids.Any(), s =>
                            (input.Parents.Exclude && (!s.ParentId.HasValue || !input.Parents.Ids.Contains(s.ParentId))) ||
                            (!input.Parents.Exclude && input.Parents.Ids.Contains(s.ParentId)))
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(input.ExcludeSubAccount, s => !s.ParentId.HasValue)
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<FindChartOfAccountDto>();
            if (totalCount > 0)
            {
                var isDefaultLanguage = await IsDefaultLagnuageAsync();

                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindChartOfAccountDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    AccountType = l.AccountType.GetName(),
                    SubAccountType = l.SubAccountType.GetName(),
                    ParentAccount = !l.ParentId.HasValue ? "" : isDefaultLanguage ? l.Parent.Name : l.Parent.DisplayName,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindChartOfAccountDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_View, PermissionNames.Pages_Accounting_ChartOfAccounts_Edit)]
        public async Task<ChartOfAccountDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _chartOfAccountRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new ChartOfAccountDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Code = l.Code,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            IsActive = l.IsActive,
                            AccountType = l.AccountType,
                            SubAccountType = l.SubAccountType,
                            AccountTypeName = l.AccountType.GetName(),
                            SubAccountTypeName = l.SubAccountType.GetName(),
                            ParentId = l.ParentId,
                            ParentAccountName = !l.ParentId.HasValue ? "" : isDefaultLanguage ? l.Parent.Name : l.Parent.DisplayName,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _chartOfAccountManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts)]
        public async Task<PagedResultDto<ChartOfAccountListDto>> GetList(PageChartOfAccountInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<ChartOfAccountListDto>> GetListHelper(PageChartOfAccountInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _chartOfAccountRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.AccountTypes != null && !input.AccountTypes.Ids.IsNullOrEmpty(), s =>
                            (input.AccountTypes.Exclude && (!input.AccountTypes.Ids.Contains(s.AccountType))) ||
                            (!input.AccountTypes.Exclude && input.AccountTypes.Ids.Contains(s.AccountType)))
                        .WhereIf(input.SubAccountTypes != null && !input.SubAccountTypes.Ids.IsNullOrEmpty(), s =>
                            (input.SubAccountTypes.Exclude && (!input.SubAccountTypes.Ids.Contains(s.SubAccountType))) ||
                            (!input.SubAccountTypes.Exclude && input.SubAccountTypes.Ids.Contains(s.SubAccountType)))
                        .WhereIf(input.Parents != null && input.Parents.Ids != null && input.Parents.Ids.Any(), s =>
                            (input.Parents.Exclude && (!s.ParentId.HasValue || !input.Parents.Ids.Contains(s.ParentId))) ||
                            (!input.Parents.Exclude && input.Parents.Ids.Contains(s.ParentId)))
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
            var items = new List<ChartOfAccountListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new ChartOfAccountListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    CannotDelete = l.CannotDelete,
                    CannotEdit = l.CannotEdit,
                    IsActive = l.IsActive,
                    AccountType = l.AccountType.GetName(),
                    SubAccountType = l.SubAccountType.GetName(),
                    ParentId = l.ParentId,
                    ParentAccountName = !l.ParentId.HasValue ? "" : isDefaultLanguage ? l.Parent.Name : l.Parent.DisplayName,
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

            return new PagedResultDto<ChartOfAccountListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelChartOfAccountInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            
            PagedResultDto<ChartOfAccountListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "ChartOfAccount.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _chartOfAccountManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _chartOfAccountManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Accounting_ChartOfAccounts_Edit)]
        public async Task Update(CreateUpdateChartOfAccountInputDto input)
        {
            var entity = MapEntity<ChartOfAccount, Guid>(input);

            CheckErrors(await _chartOfAccountManager.UpdateAsync(entity));
        }
    }
}
