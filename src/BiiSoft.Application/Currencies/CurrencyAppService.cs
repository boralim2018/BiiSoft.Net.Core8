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
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Extensions;
using BiiSoft.FileStorages;
using BiiSoft.Folders;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.Entities;

namespace BiiSoft.Currencies
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class CurrencyAppService : BiiSoftAppServiceBase, ICurrencyAppService
    {
        private readonly ICurrencyManager _currencyManager;
        private readonly IBiiSoftRepository<Currency, long> _currencyRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CurrencyAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            ICurrencyManager currencyManager,
            IBiiSoftRepository<Currency, long> currencyRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _currencyManager=currencyManager;
            _currencyRepository=currencyRepository;
            _userRepository=userRepository;
            _fileStorageManager=fileStorageManager;
            _appFolders=appFolders;
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
            var query = from l in _currencyRepository.GetAll()
                                .AsNoTracking()
                                .Where(s => s.Id == input.Id)
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new CurrencyDetailDto
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
                            CreatorUserName = u.UserName,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = m == null ? "" : m.UserName
                        };

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            var record = await _currencyRepository.GetAll()
                               .AsNoTracking()
                               .OrderBy(s => s.Id)
                               .GroupBy(s => 1)
                               .Select(s => new
                               {
                                   First = s.Where(r => r.Id < result.Id).Select(n => n.Id).OrderBy(o => o).FirstOrDefault(),
                                   Pervious = s.Where(r => r.Id < result.Id).Select(n => n.Id).OrderByDescending(o => o).FirstOrDefault(),
                                   Next = s.Where(r => r.Id > result.Id).Select(n => n.Id).OrderBy(o => o).FirstOrDefault(),
                                   Last = s.Where(r => r.Id > result.Id).Select(n => n.Id).OrderByDescending(o => o).FirstOrDefault(),
                               })
                               .FirstOrDefaultAsync();

            if (record.First > 0) result.FirstId = record.First;
            if (record.Pervious > 0) result.PreviousId = record.Pervious;
            if (record.Next > 0) result.NextId = record.Next;
            if (record.Last > 0) result.LastId = record.Last;

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies)]
        public async Task<PagedResultDto<CurrencyListDto>> GetList(PageCurrencyInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<CurrencyListDto>> GetListHelper(PageCurrencyInputDto input)
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
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new CurrencyListDto
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
                            CreatorUserName = u.UserName,
                            LastModifierUserId = u.LastModifierUserId,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserName = m == null ? "" : m.UserName,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<CurrencyListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<CurrencyListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelCurrencyInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            var result = new ExportFileOutput
            {
                FileName = "Currency.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            PagedResultDto<CurrencyListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = input.Columns.
                    Where(s => s.Visible)
                    .OrderBy(s => s.Index)
                    .ToList();

                //foreach (var i in displayColumns)
                //{
                //    ws.AddTextToCell(rowTableHeader, colHeaderTable, i.ColumnTitle, true);
                //    if (i.Width > 0) ws.Column(colHeaderTable).Width = i.Width.PixcelToInches();

                //    colHeaderTable += 1;
                //}
                #endregion Row 1

                var rowIndex = rowTableHeader + 1;
                foreach (var row in listResult.Items)
                {
                    var colIndex = 1;
                    foreach (var col in displayColumns)
                    {
                        var value = row.GetType().GetProperty(col.ColumnName.ToPascalCase()).GetValue(row);

                        if (col.ColumnName == "CreatorUserName")
                        {
                            var newValue = value;
                            if(row.CreationTime.HasValue) newValue += $"\r\n{Convert.ToDateTime(row.CreationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                            col.WriteCell(ws, rowIndex, colIndex, newValue);
                        }
                        else if (col.ColumnName == "LastModifierUserName")
                        {
                            var newValue = value;
                            if(row.LastModificationTime.HasValue) newValue += $"\r\n{Convert.ToDateTime(row.LastModificationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                            col.WriteCell(ws, rowIndex, colIndex, newValue);
                        }
                        else
                        {
                            col.WriteCell(ws, rowIndex, colIndex, value);
                        }

                        colIndex++;
                    }
                    rowIndex++;
                }

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, rowIndex - 1);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            var result = new ExportFileOutput
            {
                FileName = "Currency.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = new List<ColumnOutput> { 
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 200, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Currency")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Symbol"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Default"), Width = 150 }
                };

                #endregion Row 1

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, 5);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<long>, long>(input);

            CheckErrors(await _currencyManager.ImportAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Currencies_Edit)]
        public async Task Update(CreateUpdateCurrencyInputDto input)
        {
            var entity = MapEntity<Currency, long>(input);

            CheckErrors(await _currencyManager.UpdateAsync(entity));
        }
    }
}
