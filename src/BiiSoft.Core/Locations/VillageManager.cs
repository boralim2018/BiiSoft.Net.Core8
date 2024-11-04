using Abp.Domain.Uow;
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
using BiiSoft.Folders;
using OfficeOpenXml;
using BiiSoft.Columns;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Locations
{
    public class VillageManager : BiiSoftNameActiveValidateServiceBase<Village, Guid>, IVillageManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        private readonly IBiiSoftRepository<KhanDistrict, Guid> _khanDistrictRepository;
        private readonly IBiiSoftRepository<SangkatCommune, Guid> _sangkatCommuneRepository;
        private readonly IAppFolders _appFolders;
        public VillageManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<Village, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _countryRepository = countryRepository;
            _cityProvinceRepository = cityProvinceRepository;
            _khanDistrictRepository = khanDistrictRepository;
            _sangkatCommuneRepository = sangkatCommuneRepository;
            _appFolders = appFolders;
        }

        #region override

        protected override string InstanceName => L("Village");

        protected override void ValidateInput(Village input)
        {
            ValidateCodeInput(input.Code);
            if (input.Code.Length > BiiSoftConsts.VillageCodeLength) InvalidCodeException(input.Code);

            base.ValidateInput(input);

            ValidateSelect(input.CountryId, L("Country"));
            ValidateSelect(input.CityProvinceId, L("CityProvince"));
            ValidateSelect(input.KhanDistrictId, L("KhanDistrict"));
            ValidateSelect(input.SangkatCommuneId, L("SangkatCommune"));
        }

        protected override async Task ValidateInputAsync(Village input)
        {
            ValidateInput(input);

            var validateCountry = await _countryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CountryId);
            if (!validateCountry) InvalidException(L("Couuntry"));

            var validateProvince = await _cityProvinceRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CityProvinceId);
            if (!validateProvince) InvalidException(L("CityProvince"));

            var validateKhanDistrict = await _khanDistrictRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.KhanDistrictId);
            if (!validateKhanDistrict) InvalidException(L("KhanDistrict"));

            var validateSangkatCommune = await _sangkatCommuneRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.SangkatCommuneId);
            if (!validateSangkatCommune) InvalidException(L("SangkatCommune"));

            var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.Code == input.Code);
            if (find) DuplicateCodeException(input.Code);
        }

        protected override Village CreateInstance(Village input)
        {
            return Village.Create(input.TenantId, input.CreatorUserId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId, input.KhanDistrictId, input.SangkatCommuneId);
        }

        protected override void UpdateInstance(Village input, Village entity)
        {
            entity.Update(input.LastModifierUserId, input.Code, input.Name, input.DisplayName, input.CountryId, input.CityProvinceId, input.KhanDistrictId, input.SangkatCommuneId);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var result = new ExportFileOutput
            {
                FileName = $"{InstanceName}.xlsx",
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
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Village")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_",L("Country")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("CityProvince")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("KhanDistrict")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("SangkatCommune")), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
                };

                #endregion Row 1

                ws.InsertTable(displayColumns, $"VillageTable", rowTableHeader, 1, 5);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
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
            var villages = new List<Village>();
            var villageHash = new HashSet<string>();
            var countryDic = new Dictionary<string, Guid>();
            var cityProvinceDic = new Dictionary<string, Guid>();
            var khanDistrictDic = new Dictionary<string, Guid>();
            var sangkatCommuneDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                cityProvinceDic = await _cityProvinceRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                khanDistrictDic = await _khanDistrictRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                sangkatCommuneDic = await _sangkatCommuneRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
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
                        if (code.Length > BiiSoftConsts.VillageCodeLength) InvalidCodeException(code, $", Row = {i}");
                        if (villageHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

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

                        Guid? sangkatCommuneId = null;
                        var sangkatCommuneCode = worksheet.GetString(i, 7);
                        ValidateInput(sangkatCommuneCode, L("Code_", L("SangkatCommune")), $" , Row = {i}");
                        if (!sangkatCommuneDic.ContainsKey(sangkatCommuneCode)) InvalidException(L("Code_", L("SangkatCommune")), $", Row = {i}");
                        sangkatCommuneId = sangkatCommuneDic[sangkatCommuneCode];

                    
                        var cannotEdit = worksheet.GetBool(i, 8);
                        var cannotDelete = worksheet.GetBool(i, 9);

                        var entity = Village.Create(input.TenantId.Value, input.UserId, code, name, displayName, countryId, cityProvinceId, khanDistrictId, sangkatCommuneId);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        villages.Add(entity);
                        villageHash.Add(code);
                    }
                }
            }

            if (!villages.Any()) return IdentityResult.Success;

            var updateVillageDic = new Dictionary<string, Village>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateVillageDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => villageHash.Contains(s.Code))
                                              .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addVillages = new List<Village>();

            foreach (var l in villages)
            {
                if (updateVillageDic.ContainsKey(l.Code))
                {
                    updateVillageDic[l.Code].Update(input.UserId, l.Code, l.Name, l.DisplayName, l.CountryId, l.CityProvinceId, l.KhanDistrictId, l.SangkatCommuneId);
                    updateVillageDic[l.Code].SetCannotEdit(l.CannotEdit);
                    updateVillageDic[l.Code].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addVillages.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateVillageDic.Any()) await _repository.BulkUpdateAsync(updateVillageDic.Values.ToList());
                if (addVillages.Any()) await _repository.BulkInsertAsync(addVillages);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
