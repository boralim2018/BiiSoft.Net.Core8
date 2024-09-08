using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.Currencies.Dto;
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
using BiiSoft.FileStorages;
using BiiSoft.Folders;
using Microsoft.AspNetCore.Mvc;

namespace BiiSoft.Currencies
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class CurrencyAppService : BiiSoftExcelAppServiceBase, ICurrencyAppService
    {
        private readonly ICurrencyManager _currencyManager;
        private readonly IBiiSoftRepository<Currency, long> _currencyRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CurrencyAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            ICurrencyManager currencyManager,
            IBiiSoftRepository<Currency, long> currencyRepository,
            IBiiSoftRepository<User, long> userRepository) 
        : base(fileStorageManager, appFolders)
        {
            _currencyManager=currencyManager;
            _currencyRepository=currencyRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_Create)]
        public async Task<long> Create(CreateUpdateCurrencyInputDto input)
        {
            var entity = MapEntity<Currency, long>(input);
            
            CheckErrors(await _currencyManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _currencyManager.DeleteAsync(input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_Disable)]
        public async Task Disable(EntityDto<long> input)
        {
            var entity = MapEntity<UserEntity<long>, long>(input);

            CheckErrors(await _currencyManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_Enable)]
        public async Task Enable(EntityDto<long> input)
        {
            var entity = MapEntity<UserEntity<long>, long>(input);

            CheckErrors(await _currencyManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<long> input)
        {
            var entity = MapEntity<UserEntity<long>, long>(input);

            CheckErrors(await _currencyManager.SetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Currencies)]
        public async Task<PagedResultDto<FindCurrencyDto>> Find(PageCurrencyInputDto input)
        {
            var query = from l in _currencyRepository.GetAll()
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
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        select new FindCurrencyDto
                        {
                            Id = l.Id,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            Code = l.Code,
                            Symbol = l.Symbol,
                            IsActive = l.IsActive,
                            IsDefault = l.IsDefault                            
                        };

            var totalCount = await query.CountAsync();
            var items = new List<FindCurrencyDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<FindCurrencyDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_View, PermissionNames.Pages_Setup_Currencies_Edit)]
        public async Task<CurrencyDetailDto> GetDetail(EntityDto<long> input)
        {
            var query = _currencyRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new CurrencyDetailDto
                        {
                            Id = l.Id,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            Code = l.Code,
                            Symbol = l.Symbol,
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

            var record = await _currencyRepository.GetAll()
                               .AsNoTracking()
                               .Where(s => s.Id != result.Id)
                               .GroupBy(s => 1)
                               .Select(s => new
                               {
                                   First = s.Where(r => r.Id < result.Id).OrderBy(o => o.Id).Select(n => n.Id).FirstOrDefault(),
                                   Pervious = s.Where(r => r.Id < result.Id).OrderByDescending(o => o.Id).Select(n => n.Id).FirstOrDefault(),
                                   Next = s.Where(r => r.Id > result.Id).OrderBy(o => o.Id).Select(n => n.Id).FirstOrDefault(),
                                   Last = s.Where(r => r.Id > result.Id).OrderByDescending(o => o.Id).Select(n => n.Id).FirstOrDefault(),
                               })
                               .FirstOrDefaultAsync();

            if (record != null && record.First > 0) result.FirstId = record.First;
            if (record != null && record.Pervious > 0) result.PreviousId = record.Pervious;
            if (record != null && record.Next > 0) result.NextId = record.Next;
            if (record != null && record.Last > 0) result.LastId = record.Last;

            //await _currencyManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies)]
        public async Task<PagedResultDto<CurrencyListDto>> GetList(PageCurrencyInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<CurrencyListDto>> GetListHelper(PageCurrencyInputDto input)
        {
            var query = _currencyRepository.GetAll()
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


            var items = new List<CurrencyListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new CurrencyListDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Code = l.Code,
                    Symbol = l.Symbol,
                    IsDefault = l.IsDefault,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModifierUserId = l.LastModifierUserId,
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<CurrencyListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelCurrencyInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
           
            PagedResultDto<CurrencyListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "Currency.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _currencyManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<long>, long>(input);

            CheckErrors(await _currencyManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_Edit)]
        public async Task Update(CreateUpdateCurrencyInputDto input)
        {
            var entity = MapEntity<Currency, long>(input);

            CheckErrors(await _currencyManager.UpdateAsync(entity));
        }
    }
}
