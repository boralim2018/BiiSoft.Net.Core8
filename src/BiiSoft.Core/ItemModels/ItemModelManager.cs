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
using BiiSoft.Entities;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;
using BiiSoft.ChartOfAccounts;
using BiiSoft.Items;

namespace BiiSoft.ItemModels
{
    public class ItemModelManager : BiiSoftDefaultNameActiveValidateServiceBase<ItemModel, Guid>, IItemModelManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAppFolders _appFolders;
        public ItemModelManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ChartOfAccount, Guid> chartOfAccountRepository,
            IBiiSoftRepository<ItemModel, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _appFolders = appFolders;
        }

        #region override
        protected override string InstanceName => L("ItemModel");
        protected override bool IsUniqueName => true;

        protected override ItemModel CreateInstance(ItemModel input)
        {
            return ItemModel.Create(input.TenantId, input.CreatorUserId.Value, input.Name, input.DisplayName);
        }

        protected override void UpdateInstance(ItemModel input, ItemModel entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.Name, input.DisplayName);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var result = new ExportFileOutput
            {
                FileName = $"ItemModel.xlsx",
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
                    new ColumnOutput{ ColumnTitle = L("Name_",L("ItemModel")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Default"), Width = 150 },
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
            var itemModels = new List<ItemModel>();
            var itemModelHash = new HashSet<string>();
           
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
                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, $", Row = {i}");
                        if (itemModelHash.Contains(name)) DuplicateCodeException(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        var isDefault = worksheet.GetBool(i, 3);

                        var entity = ItemModel.Create(input.TenantId.Value, input.UserId.Value, name, displayName);
                        entity.SetDefault(isDefault);

                        itemModels.Add(entity);
                        itemModelHash.Add(name);
                    }
                }
            }

            if (!itemModels.Any()) return IdentityResult.Success;

            var updateItemModelDic = new Dictionary<string, ItemModel>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateItemModelDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => itemModelHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
            }

            var addItemModels = new List<ItemModel>();

            foreach (var l in itemModels)
            {
                if (updateItemModelDic.ContainsKey(l.Name))
                {
                    updateItemModelDic[l.Name].Update(input.UserId.Value, l.Name, l.DisplayName);
                    updateItemModelDic[l.Name].SetDefault(l.IsDefault);
                }
                else
                {
                    addItemModels.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateItemModelDic.Any()) await _repository.BulkUpdateAsync(updateItemModelDic.Values.ToList());
                if (addItemModels.Any()) await _repository.BulkInsertAsync(addItemModels);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
