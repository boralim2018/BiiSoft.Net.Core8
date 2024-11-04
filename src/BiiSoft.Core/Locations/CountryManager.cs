using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Timing;
using BiiSoft.Currencies;
using BiiSoft.FileStorages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BiiSoft.Extensions;
using BiiSoft.Entities;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Locations
{
    public class CountryManager : BiiSoftNameActiveValidateServiceBase<Country, Guid>, ICountryManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Currency, long> _currencyRepository;
        private readonly IAppFolders _appFolders;
        public CountryManager(
            IAppFolders appFolders,
            IBiiSoftRepository<Currency, long> currencyRepository,
            IBiiSoftRepository<Country, Guid> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager) : base(repository)
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _currencyRepository = currencyRepository;
            _appFolders = appFolders;
        }

        #region override
        protected override string InstanceName => L("Country");

        protected override void ValidateInput(Country input)
        {
            base.ValidateInput(input);

            ValidateCodeInput(input.Code);
            if (input.Code.ToString().Length != BiiSoftConsts.CountryCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.CountryCodeLength);
            ValidateInput(input.ISO, L("Code_", L("ISO")));
            if (input.ISO.Length != 3) EqualCharactersException(L("Code_", L("ISO")), 3);
            ValidateInput(input.ISO2, L("Code_", L("ISO2")));
            if (input.ISO2.Length != 2) EqualCharactersException(L("Code_", L("ISO2")), 2);
        }

        protected override async Task ValidateInputAsync(Country input)
        {
            ValidateInput(input);

            if (input.CurrencyId.HasValue)
            {
                var validateCurrency = await _currencyRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CurrencyId.Value);
                if (!validateCurrency) InvalidException(L("Currency"));
            }

            var find = await _repository.GetAll().AsNoTracking()
                             .FirstOrDefaultAsync(s => s.Id != input.Id && (s.Code == input.Code || s.Code == input.Code || s.ISO == input.ISO || s.Name == input.Name));

            if (find != null && find.Code == input.Code) DuplicateException($"{L("Code_", L("Location"))} : {input.Code}");
            if (find != null && find.Name == input.Name) DuplicateNameException(input.Name);
            if (find != null && find.Code == input.Code) DuplicateCodeException(input.Code.ToString());
            if (find != null && find.ISO == input.ISO) throw DuplicateException($"{L("Code_", L("ISO"))} : {input.ISO}");
        }

        protected override Country CreateInstance(Country input)
        {
            return Country.Create(input.CreatorUserId, input.Code, input.Name, input.DisplayName, input.ISO2, input.ISO, input.PhonePrefix, input.CurrencyId);
        }

        protected override void UpdateInstance(Country input, Country entity)
        {
            entity.Update(input.LastModifierUserId, input.Code, input.Name, input.DisplayName, input.ISO2, input.ISO, input.PhonePrefix, input.CurrencyId);
        }
        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var result = new ExportFileOutput
            {
                FileName = $"Country.xlsx",
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
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Country")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ISO"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ISO2"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("PhonePrefix"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Currency"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
                };

                #endregion Row 1

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, 5);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
        }


        /// <summary>
        /// Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<Guid> input)
        {
            var countries = new List<Country>();
            var countryHash = new HashSet<string>();
            var nameHash = new HashSet<string>();
            var isoHash = new HashSet<string>();
            var currencyDic = new Dictionary<string, long>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                currencyDic = await _currencyRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
            }

            //var excelPackage = Read(input, _appFolders);
            var excelPackage = await _fileStorageManager.DownloadExcel(input.Token);
            if (excelPackage != null)
            {
                // Get the work book in the file
                var workBook = excelPackage.Workbook;
                if (workBook != null)
                {
                    // retrive first worksheets
                    var worksheet = excelPackage.Workbook.Worksheets[0];
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        string code = worksheet.GetString(i, 1);
                        ValidateCodeInput(code, $", Row = {i}");
                        if (code.Length != BiiSoftConsts.CountryCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.CountryCodeLength, $" : {code}, Row = {i}");
                        if (countryHash.Contains(code)) DuplicateException(L("Code_", InstanceName), $" : {code}, Row = {i}");

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row = {i}");
                        if (nameHash.Contains(name)) DuplicateNameException(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        var iso = worksheet.GetString(i, 4);
                        ValidateInput(iso, L("Code_", L("ISO")), $", Row = {i}");
                        if (iso.Length != 3) EqualCharactersException(L("Code_", L("ISO")), 3, $", Row = {i}");
                        if (isoHash.Contains(iso)) DuplicateException(L("Code_", L("ISO")), $" : {iso}, Row = {i}");

                        var iso2 = worksheet.GetString(i, 5);
                        ValidateInput(iso2, L("Code_", L("ISO2")), $", Row = {i}");
                        if (iso2.Length != 2) EqualCharactersException(L("Code_", L("ISO2")), 2, $", Row = {i}");

                        var phonePrefix = worksheet.GetString(i, 6);

                        long? currencyId = null;
                        var currencyCode = worksheet.GetString(i, 7);
                        if (!currencyCode.IsNullOrWhiteSpace())
                        {
                            if (!currencyDic.ContainsKey(currencyCode)) InvalidException(L("Code_", L("Currency")), $", Row = {i}");

                            currencyId = currencyDic[currencyCode];
                        }

                        var cannotEdit = worksheet.GetBool(i, 8);
                        var cannotDelete = worksheet.GetBool(i, 9);

                        var entity = Country.Create(input.UserId, code, name, displayName, iso2, iso, phonePrefix, currencyId);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        countries.Add(entity);
                        countryHash.Add(code);
                        isoHash.Add(iso);
                    }
                }
            }

            if (!countries.Any()) return IdentityResult.Success;

            var updateCountryDic = new Dictionary<string, Country>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateCountryDic = await _repository.GetAll().AsNoTracking()
                                        .Where(s => countryHash.Contains(s.Code))
                                        .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addCountrys = new List<Country>();

            foreach (var l in countries)
            {
                if (updateCountryDic.ContainsKey(l.Code))
                {
                    updateCountryDic[l.Code].Update(input.UserId, l.Code, l.Name, l.DisplayName, l.ISO2, l.ISO, l.PhonePrefix, l.CurrencyId);
                    updateCountryDic[l.Code].SetCannotEdit(l.CannotEdit);
                    updateCountryDic[l.Code].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addCountrys.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateCountryDic.Any()) await _repository.BulkUpdateAsync(updateCountryDic.Values.ToList());
                if (addCountrys.Any()) await _repository.BulkInsertAsync(addCountrys);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
