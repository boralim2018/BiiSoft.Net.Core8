using Abp.Domain.Uow;
using Abp.UI;
using BiiSoft.BFiles.Dto;
using BiiSoft.Columns;
using BiiSoft.Entities;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using BiiSoft.FileStorages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BiiSoft.Locations
{
    public class KhanDistrictManager : BiiSoftNameActiveValidateServiceBase<KhanDistrict, Guid>, IKhanDistrictManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        private readonly IExcelManager _excelManager;
        public KhanDistrictManager(
            IExcelManager excelManager,
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
            _excelManager = excelManager;
        }

        #region override
        protected override string InstanceName => L("KhanDistrict");

        protected override void ValidateInput(KhanDistrict input)
        {
            ValidateCodeInput(input.Code);
            if (input.Code.Length != BiiSoftConsts.KhanDistrictCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.KhanDistrictCodeLength);

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

        protected override KhanDistrict CreateInstance(KhanDistrict input)
        {
            return KhanDistrict.Create(input.TenantId, input.CreatorUserId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId);
        }

        protected override void UpdateInstance(KhanDistrict input, KhanDistrict entity)
        {
            entity.Update(input.LastModifierUserId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"KhanDistrict.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 200, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("KhanDistrict")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("Country")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("CityProvince")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
                }
            };

            return await _excelManager.ExportExcelTemplateAsync(fileInput);
        }

        /// <summary>
        ///  Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<Guid> input)
        {
            var khanDistricts = new List<KhanDistrict>();
            var khanDistrictHash = new HashSet<string>();
            var countryDic = new Dictionary<string, Guid>();
            var cityProvinceDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                    cityProvinceDic = await _cityProvinceRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                }
            }

            
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
                        if (code.Length != BiiSoftConsts.KhanDistrictCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.KhanDistrictCodeLength, $", Row = {i}");
                        if (khanDistrictHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        Guid? countryId = null;
                        var countryCode = worksheet.GetString(i, 4);
                        ValidateInput(countryCode, L("Code_", L("Country")), $", Row = {i}");
                        if (!countryDic.ContainsKey(countryCode)) InvalidException(L("Code_", L("Currency")), $", Row = {i}");
                        countryId = countryDic[countryCode];

                        Guid? cityProvinceId = null;
                        var cityProvinceCode = worksheet.GetString(i, 5);
                        ValidateInput(cityProvinceCode, L("Code_", L("CityProvince")), $", Row = {i}");
                        if (!cityProvinceDic.ContainsKey(cityProvinceCode)) InvalidException(L("Code_", L("CityProvince")), $", Row = {i}");
                        cityProvinceId = cityProvinceDic[cityProvinceCode];

                        var cannotEdit = worksheet.GetBool(i, 6);
                        var cannotDelete = worksheet.GetBool(i, 7); 

                        var entity = KhanDistrict.Create(input.TenantId.Value, input.UserId, code, name, displayName, countryId, cityProvinceId);
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
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    updateKhanDistrictDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => khanDistrictHash.Contains(s.Code))
                                              .ToDictionaryAsync(k => k.Code, v => v);
                }
            }

            var addKhanDistricts = new List<KhanDistrict>();

            foreach (var l in khanDistricts)
            {
                if (updateKhanDistrictDic.ContainsKey(l.Code))
                {
                    updateKhanDistrictDic[l.Code].Update(input.UserId, l.Code, l.Name, l.DisplayName, l.CountryId, l.CityProvinceId);
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
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    if (updateKhanDistrictDic.Any()) await _repository.BulkUpdateAsync(updateKhanDistrictDic.Values.ToList());
                    if (addKhanDistricts.Any()) await _repository.BulkInsertAsync(addKhanDistricts);
                }
                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
