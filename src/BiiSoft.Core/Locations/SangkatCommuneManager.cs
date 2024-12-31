using Abp.Domain.Uow;
using BiiSoft.BFiles.Dto;
using BiiSoft.Columns;
using BiiSoft.Entities;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using BiiSoft.FileStorages;
using BiiSoft.Folders;
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
    public class SangkatCommuneManager : BiiSoftNameActiveValidateServiceBase<SangkatCommune, Guid>, ISangkatCommuneManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        private readonly IBiiSoftRepository<KhanDistrict, Guid> _khanDistrictRepository;
        private readonly IAppFolders _appFolders;
        private readonly IExcelManager _excelManager;
        public SangkatCommuneManager(
            IExcelManager excelManager,
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<SangkatCommune, Guid> repository) : base(repository)
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _countryRepository = countryRepository;
            _cityProvinceRepository = cityProvinceRepository;
            _khanDistrictRepository = khanDistrictRepository;
            _appFolders = appFolders;
            _excelManager = excelManager;
        }

        #region override
        protected override string InstanceName => L("SangkatCommune");

        protected override void ValidateInput(SangkatCommune input)
        {
            ValidateCodeInput(input.Code);
            if (input.Code.Length != BiiSoftConsts.SangkatCommuneCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.SangkatCommuneCodeLength);

            base.ValidateInput(input);

            ValidateSelect(input.CountryId, L("Country"));
            ValidateSelect(input.CityProvinceId, L("CityProvince"));
            ValidateSelect(input.KhanDistrictId, L("KhanDistrict"));
        }

        protected override async Task ValidateInputAsync(SangkatCommune input)
        {
            ValidateInput(input);

            var validateCountry = await _countryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CountryId);
            if (!validateCountry) InvalidException(L("Couuntry"));

            var validateProvince = await _cityProvinceRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CityProvinceId);
            if (!validateProvince) InvalidException(L("CityProvince"));

            var validateKhanDistrict = await _khanDistrictRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.KhanDistrictId);
            if (!validateKhanDistrict) InvalidException(L("KhanDistrict"));


            var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.Code == input.Code);
            if (find) DuplicateCodeException(input.Code);
        }

        protected override SangkatCommune CreateInstance(SangkatCommune input)
        {
            return SangkatCommune.Create(input.TenantId, input.CreatorUserId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId, input.KhanDistrictId);
        }

        protected override void UpdateInstance(SangkatCommune input, SangkatCommune entity)
        {
            entity.Update(input.LastModifierUserId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId, input.KhanDistrictId);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"SangkatCommune.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 200, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("SangkatCommune")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("Country")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("CityProvince")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("KhanDistrict")), Width = 150, IsRequired = true },
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
            var sangkatCommunes = new List<SangkatCommune>();
            var sangkatCommuneHash = new HashSet<string>();
            var countryDic = new Dictionary<string, Guid>();
            var cityProvinceDic = new Dictionary<string, Guid>();
            var khanDistrictDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                cityProvinceDic = await _cityProvinceRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                khanDistrictDic = await _khanDistrictRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
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
                        if (code.Length != BiiSoftConsts.SangkatCommuneCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.SangkatCommuneCodeLength, $", Row = {i}");
                        if (sangkatCommuneHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

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

                        Guid? khanDistrictId = null;
                        var khanDistrictCode = worksheet.GetString(i, 6);
                        ValidateInput(khanDistrictCode, L("Code_", L("KhanDistrict")), $", Row = {i}");
                        if (!khanDistrictDic.ContainsKey(khanDistrictCode)) InvalidException(L("Code_", L("KhanDistrict")), $", Row = {i}");
                        khanDistrictId = khanDistrictDic[khanDistrictCode];

                        var cannotEdit = worksheet.GetBool(i, 7);
                        var cannotDelete = worksheet.GetBool(i, 8);

                        var entity = SangkatCommune.Create(input.TenantId.Value, input.UserId, code, name, displayName, countryId, cityProvinceId, khanDistrictId);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        sangkatCommunes.Add(entity);
                        sangkatCommuneHash.Add(code);
                    }
                }
            }

            if (!sangkatCommunes.Any()) return IdentityResult.Success;

            var updateSangkatCommuneDic = new Dictionary<string, SangkatCommune>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateSangkatCommuneDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => sangkatCommuneHash.Contains(s.Code))
                                              .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addSangkatCommunes = new List<SangkatCommune>();

            foreach (var l in sangkatCommunes)
            {
                if (updateSangkatCommuneDic.ContainsKey(l.Code))
                {
                    updateSangkatCommuneDic[l.Code].Update(input.UserId, l.Code, l.Name, l.DisplayName, l.CountryId, l.CityProvinceId, l.KhanDistrictId);
                    updateSangkatCommuneDic[l.Code].SetCannotEdit(l.CannotEdit);
                    updateSangkatCommuneDic[l.Code].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addSangkatCommunes.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateSangkatCommuneDic.Any()) await _repository.BulkUpdateAsync(updateSangkatCommuneDic.Values.ToList());
                if (addSangkatCommunes.Any()) await _repository.BulkInsertAsync(addSangkatCommunes);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
