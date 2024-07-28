using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Timing;
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
    public class KhanDistrictManager : BiiSoftNameActiveValidateServiceBase<KhanDistrict, Guid>, IKhanDistrictManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        public KhanDistrictManager(
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<KhanDistrict, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _countryRepository = countryRepository;
            _cityProvinceRepository = cityProvinceRepository;
        }

        #region override
        protected override string InstanceName => L("KhanDistrict");

        protected override void ValidateInput(KhanDistrict input)
        {
            ValidateCodeInput(input.Code);
            if (input.Code.Length != BiiSoftConsts.LocationCodeLength) InvalidCodeException(input.Code);

            base.ValidateInput(input);

            ValidateSelect(input.CountryId, L("Country"));
            ValidateSelect(input.CityProvinceId, L("CityProvince"));
        }

        protected override async Task ValidateInputAsync(KhanDistrict input)
        {
            ValidateInput(input);

            var validateCountry = await _countryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CountryId);
            if (!validateCountry) InvalidException(L("Couuntry"));
          
            var validateProvince = await _cityProvinceRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CityProvinceId);
            if (!validateProvince) InvalidException(L("CityProvince"));

            var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.Code == input.Code);
            if (find) DuplicateCodeException(input.Code);
        }

        protected override KhanDistrict CreateInstance(int? tenantId, long userId, KhanDistrict input)
        {
            return KhanDistrict.Create(userId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId, input.Latitude, input.Longitude);
        }

        protected override void UpdateInstance(long userId, KhanDistrict input, KhanDistrict entity)
        {
            entity.Update(userId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId, input.Latitude, input.Longitude);
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
            var khanDistricts = new List<KhanDistrict>();
            var khanDistrictHash = new HashSet<string>();
            var countryDic = new Dictionary<string, Guid>();
            var cityProvinceDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.ISO, v => v.Id);
                cityProvinceDic = await _cityProvinceRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.ISO, v => v.Id);
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
                        ValidateCodeInput(code, $", Row = {i}");
                        if (code.Length != BiiSoftConsts.LocationCodeLength) InvalidCodeException(code, $", Row = {i}");
                        if (khanDistrictHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        Guid? countryId = null;
                        var countryISO = worksheet.GetString(i, 4);
                        ValidateInput(countryISO, L("Code_", L("Country")), $", Row = {i}");
                        if (!countryDic.ContainsKey(countryISO)) InvalidException(L("Code_", L("Currency")), $", Row = {i}");
                        countryId = countryDic[countryISO];

                        Guid? cityProvinceId = null;
                        var cityProvinceISO = worksheet.GetString(i, 5);
                        ValidateInput(cityProvinceISO, L("Code_", L("CityProvince")), $", Row = {i}");
                        if (!cityProvinceDic.ContainsKey(cityProvinceISO)) InvalidException(L("Code_", L("CityProvince")), $", Row = {i}");
                        cityProvinceId = cityProvinceDic[cityProvinceISO];

                        var latitude = worksheet.GetDecimalOrNull(i, 6);
                        var longitude = worksheet.GetDecimalOrNull(i, 7);
                        var cannotEdit = worksheet.GetBool(i, 8);
                        var cannotDelete = worksheet.GetBool(i, 9); 

                        var entity = KhanDistrict.Create(userId, code, name, displayName, countryId, cityProvinceId, latitude, longitude);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        khanDistricts.Add(entity);
                        khanDistrictHash.Add(code);
                    }
                }
            }

            if (!khanDistricts.Any()) return IdentityResult.Success;

            var updateKhanDistrictDic = new Dictionary<string, KhanDistrict>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateKhanDistrictDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => khanDistrictHash.Contains(s.Code))
                                              .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addKhanDistricts = new List<KhanDistrict>();

            foreach (var l in khanDistricts)
            {
                if (updateKhanDistrictDic.ContainsKey(l.Code))
                {
                    updateKhanDistrictDic[l.Code].Update(userId, l.Code, l.Name, l.DisplayName, l.CountryId, l.CityProvinceId, l.Latitude, l.Longitude);
                    updateKhanDistrictDic[l.Code].SetCannotEdit(l.CannotEdit);
                    updateKhanDistrictDic[l.Code].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addKhanDistricts.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateKhanDistrictDic.Any()) await _repository.BulkUpdateAsync(updateKhanDistrictDic.Values.ToList());
                if (addKhanDistricts.Any()) await _repository.BulkInsertAsync(addKhanDistricts);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
