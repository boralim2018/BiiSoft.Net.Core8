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
using BiiSoft.Entities;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Locations
{
    public class CityProvinceManager : BiiSoftNameActiveValidateServiceBase<CityProvince, Guid>, ICityProvinceManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IAppFolders _appFolders;
        public CityProvinceManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<CityProvince, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _countryRepository = countryRepository;
            _appFolders = appFolders;
        }

        #region override
        protected override string InstanceName => L("CityProvince");

        protected override void ValidateInput(CityProvince input)
        {
            ValidateCodeInput(input.Code);
            if (input.Code.Length != BiiSoftConsts.CityProvinceCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.CityProvinceCodeLength);

            base.ValidateInput(input);

            ValidateInput(input.ISO, L("Code_", L("ISO")));
            if (input.ISO.Length > 6) MoreThanCharactersException(L("Code_", L("ISO")), 6);
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

        protected override CityProvince CreateInstance(CityProvince input)
        {
            return CityProvince.Create(input.CreatorUserId, input.Code, input.Name, input.DisplayName, input.ISO, input.CountryId);
        }

        protected override void UpdateInstance(CityProvince input, CityProvince entity)
        {
            entity.Update(input.LastModifierUserId, input.Code, input.Name, input.DisplayName, input.ISO, input.CountryId);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var result = new ExportFileOutput
            {
                FileName = $"CityProvince.xlsx",
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
                    new ColumnOutput{ ColumnTitle = L("Name_",L("CityProvince")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ISO"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code_", L("Country")), Width = 150, IsRequired = true },
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
        ///  Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<Guid> input)
        {
            var cityProvinces = new List<CityProvince>();
            var cityProvinceHash = new HashSet<string>();
            var isoHash = new HashSet<string>();
            var countryDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
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
                        var code = worksheet.GetString(i, 1);
                        ValidateCodeInput(code, $", Row = {i}");
                        if (code.Length != BiiSoftConsts.CityProvinceCodeLength) EqualCharactersException(L("Code_", InstanceName), BiiSoftConsts.CityProvinceCodeLength, $", Row = {i}");
                        if (cityProvinceHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name);

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName);

                        var iso = worksheet.GetString(i, 4);
                        ValidateInput(iso, L("Code_", L("ISO")), $", Row = {i}");
                        if (iso.Length > 6) MoreThanCharactersException(L("Code_", L("ISO")), 6, $", Row = {i}");
                        if (isoHash.Contains(iso)) DuplicateException(L("Code_", L("ISO")), $" : {iso}, Row = {i}");

                        Guid? countryId = null;
                        var countryCode = worksheet.GetString(i, 5);
                        ValidateInput(countryCode, L("Code_", L("Country")), $", Row = {i}");
                        if (!countryDic.ContainsKey(countryCode)) InvalidException(L("Code_", L("Country")), $" : {countryCode}, Row = {i}");
                        countryId = countryDic[countryCode];

                        var cannotEdit = worksheet.GetBool(i, 6);
                        var cannotDelete = worksheet.GetBool(i, 7); 

                        var entity = CityProvince.Create(input.UserId, code, name, displayName, iso, countryId);
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
                    updateCityProvinceDic[l.Code].Update(input.UserId, l.Code, l.Name, l.DisplayName, l.ISO, l.CountryId);
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
