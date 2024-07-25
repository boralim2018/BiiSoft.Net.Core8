using Abp.Domain.Uow;
using Abp.UI;
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

namespace BiiSoft.Locations
{
    public class CityProvinceManager : BiiSoftNameActiveValidateServiceBase<CityProvince, Guid>, ICityProvinceManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        public CityProvinceManager(
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<CityProvince, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _countryRepository = countryRepository;
        }

        #region override
        protected override string InstanceName => L("CityProvince");

        protected override void ValidateInput(CityProvince input)
        {
            ValidateCodeInput(input.Code);
            if (input.Code.Length != BiiSoftConsts.LocationCodeLength) InvalidCodeException(input.Code);

            base.ValidateInput(input);

            ValidateInput(input.ISO, L("Code_", L("ISO")));
            if (input.ISO.Length > 6) MoreThanCharsException(L("Code_", L("ISO")), 6);
            ValidateSelect(input.CountryId, L("Country"));
        }

        protected override async Task ValidateInputAsync(CityProvince input)
        {
            ValidateInput(input);
          
            var validateCountry = await _countryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CountryId);
            if (!validateCountry) InvalidException(L("Country"));

            var find = await _repository.GetAll().AsNoTracking().FirstOrDefaultAsync(s => s.Id != input.Id && (s.Code == input.Code || s.ISO == input.ISO));

            if (find != null && find.Code == input.Code) DuplicateException($"{L("Code_", L("Location"))} : {input.Code}");
            if (find != null && find.ISO == input.ISO) DuplicateException($"{L("Code_", L("ISO"))} : {input.ISO}");
        }

        protected override CityProvince CreateInstance(int? tenantId, long userId, CityProvince input)
        {
            return CityProvince.Create(userId, input.Code, input.Name, input.DisplayName, input.ISO, input.CountryId, input.Latitude, input.Longitude);
        }

        protected override void UpdateInstance(long userId, CityProvince input, CityProvince entity)
        {
            entity.Update(userId, input.Code, input.Name, input.DisplayName, input.ISO, input.CountryId, input.Latitude, input.Longitude);
        }

        #endregion
              
        /// <summary>
        ///  Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportAsync(long userId, string fileToken)
        {
            var cityProvinces = new List<CityProvince>();
            var cityProvinceHash = new HashSet<string>();
            var isoHash = new HashSet<string>();
            var countryDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.ISO, v => v.Id);
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
                        var code = worksheet.GetString(i, 1);
                        ValidateCodeInput(code, $", Row = {i}");
                        if (code.Length != BiiSoftConsts.LocationCodeLength) InvalidCodeException(code, $", Row = {i}");
                        if (cityProvinceHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name);

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName);

                        var iso = worksheet.GetString(i, 4);
                        ValidateInput(iso, $"{L("Code_", L("ISO"))}, Row = {i}");
                        if (iso.Length > 6) MoreThanCharsException(L("Code_", L("ISO")), 6);
                        if (isoHash.Contains(iso)) DuplicateException($"{L("Code_", L("ISO"))} : {iso}, Row = {i}");

                        Guid? countryId = null;
                        var countryISO = worksheet.GetString(i, 5);
                        ValidateInput(countryISO, $"{L("Code_", L("Country"))}, Row = {i}");
                        if (!countryDic.ContainsKey(countryISO)) InvalidException($"{L("Code_", L("Country"))} : {countryISO}, Row = {i}");
                        countryId = countryDic[countryISO];

                        var latitude = worksheet.GetDecimalOrNull(i, 6);
                        var longitude = worksheet.GetDecimalOrNull(i, 7);
                        var cannotEdit = worksheet.GetBool(i, 8);
                        var cannotDelete = worksheet.GetBool(i, 9); 

                        var entity = CityProvince.Create(userId, code, name, displayName, iso, countryId, latitude, longitude);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        cityProvinces.Add(entity);
                        cityProvinceHash.Add(code);                      
                        isoHash.Add(iso);
                    }
                }
            }

            if (!cityProvinces.Any()) return IdentityResult.Success;

            var updateCityProvinceDic = new Dictionary<string, CityProvince>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateCityProvinceDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => cityProvinceHash.Contains(s.Code))
                                              .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addCityProvinces = new List<CityProvince>();

            foreach (var l in cityProvinces)
            {
                if (updateCityProvinceDic.ContainsKey(l.Code))
                {
                    updateCityProvinceDic[l.Code].Update(userId, l.Code, l.Name, l.DisplayName, l.ISO, l.CountryId, l.Latitude, l.Longitude);
                    updateCityProvinceDic[l.Code].SetCannotEdit(l.CannotEdit);
                    updateCityProvinceDic[l.Code].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addCityProvinces.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateCityProvinceDic.Any()) await _repository.BulkUpdateAsync(updateCityProvinceDic.Values.ToList());
                if (addCityProvinces.Any()) await _repository.BulkInsertAsync(addCityProvinces);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
