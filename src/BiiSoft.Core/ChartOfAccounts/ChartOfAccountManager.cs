using Abp.Extensions;
using BiiSoft.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.FileStorages;
using BiiSoft.Extensions;
using BiiSoft.Entities;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.ChartOfAccounts
{
    public class ChartOfAccountManager : BiiSoftNameActiveValidateServiceBase<ChartOfAccount, Guid>, IChartOfAccountManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAppFolders _appFolders;
        public ChartOfAccountManager(
            IAppFolders appFolders,
            IBiiSoftRepository<ChartOfAccount, Guid> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager): base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _appFolders = appFolders;
        }

        #region override base class
      
        protected override string InstanceName => L("ChartOfAccount");

        protected override ChartOfAccount CreateInstance(ChartOfAccount input)
        {
            return ChartOfAccount.Create(input.TenantId, input.CreatorUserId.Value, input.SubAccountType, input.Code, input.Name, input.DisplayName, input.ParentId);
        }

        protected override void UpdateInstance(ChartOfAccount input, ChartOfAccount entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.SubAccountType, input.Code, input.Name, input.DisplayName, input.ParentId);
        }

        #endregion

        private async Task<string> GetLatestCodeAsync(SubAccountType type)
        {
            var prefix = type.ToIntStr();

            return await _repository.GetAll()
                            .AsNoTracking()
                            .Where(s => s.SubAccountType == type)
                            .Where(s => s.Code.StartsWith(prefix))
                            .Where(s => s.Code.Length == BiiSoftConsts.ChartOfAccountCodeLength)
                            .Select(s => s.Code)
                            .OrderByDescending(s => s)
                            .FirstOrDefaultAsync();
        }


        private async Task SetCodeAsync(ChartOfAccount input)
        {
            if (!input.Code.IsNullOrWhiteSpace()) return;

            var prefix = input.SubAccountType.ToIntStr();
            var latestCode = await GetLatestCodeAsync(input.SubAccountType);

            if (latestCode.IsNullOrWhiteSpace())
            {
                input.SetCode(1.GenerateCode(BiiSoftConsts.ChartOfAccountCodeLength, prefix));
            }
            else
            {
                input.SetCode(latestCode.NextCode(prefix));
            }
        }

        public override async Task<IdentityResult> InsertAsync(ChartOfAccount input)
        {
            await SetCodeAsync(input);
            return await base.InsertAsync(input);
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var result = new ExportFileOutput
            {
                FileName = $"ChartOfAccount.xlsx",
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
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Account")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("SubAccountType"), Width = 150, IsRequired = true},
                    new ColumnOutput{ ColumnTitle = L("ParentAccount"), Width = 150 },
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
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<Guid> input)
        {
            var locations = new List<ChartOfAccount>();
          
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

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row: {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row: {i}");

                        var subtAccount = worksheet.GetString(i, 4);
                        ValidateInput(subtAccount, L("SubAccountType"), $", Row: {i}");

                        SubAccountType subAccountType = (SubAccountType)Enum.Parse(typeof(SubAccountType), subtAccount.Replace(" ", ""));
                        
                        Guid? parentId = null;
                        var parent = worksheet.GetString(i, 5);
                       
                        var cannotEdit = worksheet.GetBool(i, 6);
                        var cannotDelete = worksheet.GetBool(i, 7); 

                        var entity = ChartOfAccount.Create(input.TenantId.Value, input.UserId.Value, subAccountType, code, name, displayName, parentId);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        locations.Add(entity);
                    }
                }
            }

            if (!locations.Any()) return IdentityResult.Success;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                   await _repository.BulkInsertAsync(locations);
                }

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }

        //public async Task<IdentityResult> ImportAsync(int? tenantId, long userId, string fileToken)
        //{
        //    var locations = new List<Location>();
        //    var locationHash = new HashSet<string>();
        //    var latestCode = "";

        //    using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
        //    {
        //        using (_unitOfWorkManager.Current.SetTenantId(tenantId))
        //        {
        //            latestCode = await GetLatestCodeAsync();
        //        }
        //    }

        //    if (latestCode.IsNullOrWhiteSpace()) latestCode = GenerateCode(0);

        //    //var excelPackage = Read(input, _appFolders);
        //    var excelPackage = await _fileStorageManager.DownloadExcel(fileToken);
        //    if (excelPackage != null)
        //    {
        //        // Get the work book in the file
        //        var workBook = excelPackage.Workbook;
        //        if (workBook != null)
        //        {
        //            // retrive first worksheets
        //            var worksheet = excelPackage.Workbook.Worksheets[0];
        //            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
        //            {
        //                var code = worksheet.GetString(i, 1);
        //                ValidateAutoCodeIfHasValue(code, $", Row: {i}");

        //                if (code.IsNullOrWhiteSpace())
        //                {
        //                    code = latestCode.NextCode(Prefix);
        //                    ValidateCodeOutOfRange(latestCode, $", Row = {i}");

        //                    latestCode = code;
        //                }
        //                else
        //                {
        //                    latestCode = latestCode.MaxCode(code, Prefix);
        //                }

        //                if (locationHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");

        //                var name = worksheet.GetString(i, 2);
        //                ValidateName(name, $", Row: {i}");

        //                var displayName = worksheet.GetString(i, 3);
        //                ValidateDisplayName(name, $", Row: {i}");

        //                var latitude = worksheet.GetDecimalOrNull(i, 4);
        //                var longitude = worksheet.GetDecimalOrNull(i, 5);
        //                var cannotEdit = worksheet.GetBool(i, 6);
        //                var cannotDelete = worksheet.GetBool(i, 7);

        //                var entity = Location.Create(tenantId, userId, code, name, displayName, latitude, longitude);
        //                entity.SetCannotEdit(cannotEdit);
        //                entity.SetCannotDelete(cannotDelete);

        //                locations.Add(entity);
        //                locationHash.Add(entity.Code);
        //            }
        //        }
        //    }

        //    if (!locations.Any()) return IdentityResult.Success;

        //    var updateLocationDic = new Dictionary<string, Location>();

        //    using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
        //    {
        //        using (_unitOfWorkManager.Current.SetTenantId(tenantId))
        //        {
        //            updateLocationDic = await _repository.GetAll().AsNoTracking()
        //                       .Where(s => locationHash.Contains(s.Code))
        //                       .ToDictionaryAsync(k => k.Code, v => v);
        //        }
        //    }

        //    var addLocations = new List<Location>();

        //    foreach (var l in locations)
        //    {
        //        if (updateLocationDic.ContainsKey(l.Code))
        //        {
        //            updateLocationDic[l.Code].Update(userId, l.Code, l.Name, l.DisplayName, l.Latitude, l.Longitude);
        //            updateLocationDic[l.Code].SetCannotEdit(l.CannotEdit);
        //            updateLocationDic[l.Code].SetCannotDelete(l.CannotDelete);
        //        }
        //        else
        //        {
        //            addLocations.Add(l);
        //        }
        //    }

        //    using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
        //    {
        //        using (_unitOfWorkManager.Current.SetTenantId(tenantId))
        //        {
        //            if (updateLocationDic.Any()) await _repository.BulkUpdateAsync(updateLocationDic.Values.ToList());
        //            if (addLocations.Any()) await _repository.BulkInsertAsync(addLocations);
        //        }

        //        await uow.CompleteAsync();
        //    }

        //    return IdentityResult.Success;
        //}
    }
}
