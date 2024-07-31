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
using Abp.Domain.Entities;

namespace BiiSoft.Locations
{
    public class CountryManager : BiiSoftNameActiveValidateServiceBase<Country, Guid>, ICountryManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Currency, long> _currencyRepository;
        public CountryManager(
            IBiiSoftRepository<Currency, long> currencyRepository,
            IBiiSoftRepository<Country, Guid> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager) : base(repository)
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _currencyRepository = currencyRepository;
        }

        #region override
        protected override string InstanceName => L("Country");

        protected override void ValidateInput(Country input)
        {
            base.ValidateInput(input);

            ValidateCodeInput(input.Code);
            if (input.Code.ToString().Length > 3) MoreThanCharactersException(L("Code_", L("Country")), 3);
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

        protected override Country CreateInstance(long userId, Country input)
        {
            return Country.Create(userId, input.Code, input.Name, input.DisplayName, input.ISO, input.ISO, input.PhonePrefix, input.CurrencyId);
        }

        protected override void UpdateInstance(long userId, Country input, Country entity)
        {
            entity.Update(userId, input.Code, input.Name, input.DisplayName, input.ISO2, input.ISO, input.PhonePrefix, input.CurrencyId);
        }
        #endregion

        /// <summary>
        /// Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ImportAsync(long userId, string fileToken)
        {
            var countries = new List<Country>();
            var countryHash = new HashSet<int>();
            var nameHash = new HashSet<string>();
            var locationHash = new HashSet<string>();
            var isoHash = new HashSet<string>();
            var currencyDic = new Dictionary<string, long>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                currencyDic = await _currencyRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
            }

            //var excelPackage = Read(input, _appFolders);
            var excelPackage = await _fileStorageManager.DownloadExcel(fileToken);
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
                        ValidateInput(code, L("Code_", L("Location")), $", Row = {i}");
                        if (code.Length != BiiSoftConsts.LocationCodeLength) InvalidException(L("Code_", L("Location")), $" : {code}, Row = {i}");
                        if (locationHash.Contains(code)) DuplicateException(L("Code_", L("Location")), $" : {code}, Row = {i}");

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row = {i}");
                        if (nameHash.Contains(name)) DuplicateNameException(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        int countryCode = Convert.ToInt32(worksheet.Cells[i, 4].Value??0);
                        ValidateCodeInput(countryCode, $", Row = {i}");
                        if (countryCode.ToString().Length > 3) MoreThanCharactersException(L("Code_", L("Country")), 3, $", Row = {i}");
                        if (countryHash.Contains(countryCode)) DuplicateCodeException(countryCode.ToString(), $", Row = {i}");
                        
                        var iso = worksheet.GetString(i, 5);
                        ValidateInput(iso, L("Code_", L("ISO")), $", Row = {i}");
                        if (iso.Length != 3) EqualCharactersException(L("Code_", L("ISO")), 3, $", Row = {i}");
                        if (isoHash.Contains(iso)) DuplicateException(L("Code_", L("ISO")), $" : {iso}, Row = {i}");

                        var iso2 = worksheet.GetString(i, 6);
                        ValidateInput(iso2, L("Code_", L("ISO2")), $", Row = {i}");
                        if (iso2.Length != 2) EqualCharactersException(L("Code_", L("ISO2")), 2, $", Row = {i}");

                        var phonePrefix = worksheet.GetString(i, 7);

                        long? currencyId = null;
                        var currencyCode = worksheet.GetString(i, 8);
                        if (!currencyCode.IsNullOrWhiteSpace())
                        {
                            if (!currencyDic.ContainsKey(currencyCode)) InvalidException(L("Code_", L("Currency")), $", Row = {i}");

                            currencyId = currencyDic[currencyCode];
                        }

                        var latitude = worksheet.GetDecimalOrNull(i, 9);
                        var longitude = worksheet.GetDecimalOrNull(i, 10);
                        var cannotEdit = worksheet.GetBool(i, 11);
                        var cannotDelete = worksheet.GetBool(i, 12);

                        var entity = Country.Create(userId, code, name, displayName, iso2, iso, phonePrefix, currencyId);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        countries.Add(entity);
                        locationHash.Add(code);
                        countryHash.Add(countryCode);
                        isoHash.Add(iso);
                    }
                }
            }

            if (!countries.Any()) return IdentityResult.Success;

            var updateCountryDic = new Dictionary<string, Country>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateCountryDic = await _repository.GetAll().AsNoTracking()
                                        .Where(s => locationHash.Contains(s.Code))
                                        .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addCountrys = new List<Country>();

            foreach (var l in countries)
            {
                if (updateCountryDic.ContainsKey(l.Code))
                {
                    updateCountryDic[l.Code].Update(userId, l.Code, l.Name, l.DisplayName, l.ISO2, l.ISO, l.PhonePrefix, l.CurrencyId);
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
