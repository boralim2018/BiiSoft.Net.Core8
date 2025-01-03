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
using BiiSoft.Excels;

namespace BiiSoft.Locations
{
    public class LocationManager : BiiSoftNameActiveValidateServiceBase<Location, Guid>, ILocationManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAppFolders _appFolders;
        private readonly IExcelManager _excelManager;
        public LocationManager(
            IExcelManager excelManager,
            IAppFolders appFolders,
            IBiiSoftRepository<Location, Guid> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager): base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _appFolders = appFolders;
            _excelManager = excelManager;
        }

        #region override base class
      
        protected override string InstanceName => L("Location");

        protected override Location CreateInstance(Location input)
        {
            return Location.Create(input.TenantId, input.CreatorUserId.Value, input.Name, input.DisplayName, input.Latitude, input.Longitude);
        }

        protected override void UpdateInstance(Location input, Location entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.Name, input.DisplayName, input.Latitude, input.Longitude);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"Location.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Location")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Latitude"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Longitude"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
                }
            };

            return await _excelManager.ExportExcelTemplateAsync(fileInput);
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
            var locations = new List<Location>();
          
            
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
                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, $", Row: {i}");

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, $", Row: {i}");

                        var latitude = worksheet.GetDecimalOrNull(i, 3);
                        var longitude = worksheet.GetDecimalOrNull(i, 4);
                        var cannotEdit = worksheet.GetBool(i, 5);
                        var cannotDelete = worksheet.GetBool(i, 6); 

                        var entity = Location.Create(input.TenantId.Value, input.UserId.Value, name, displayName, latitude, longitude);
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

        //    
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
