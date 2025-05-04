using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using Amazon.S3.Model;
using BiiSoft.BFiles.Dto;
using BiiSoft.Columns;
using BiiSoft.Entities;
using BiiSoft.Enums;
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

namespace BiiSoft.BOMs
{
    public class BOMManager : BiiSoftDefaultNameActiveValidateServiceBase<BOM, Guid>, IBOMManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        private readonly IBiiSoftRepository<BOMItem, Guid> _bomItemRepository;

        public BOMManager(
            IExcelManager excelManager,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<BOMItem, Guid> bomItemRepository,
            IBiiSoftRepository<BOM, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _excelManager = excelManager;
            _bomItemRepository = bomItemRepository;
        }

        #region override
        protected string InstanceKeyName => "BOM"; 
        protected override string InstanceName => L(InstanceKeyName);
        protected override bool IsUniqueName => true;

        protected override void ValidateInput(BOM input)
        {   
            base.ValidateInput(input);

            if (input.BOMItems.IsNullOrEmpty()) RequiredException(L("BOMItems"));

            var findItem = input.BOMItems.Any(s => s.ItemId == input.ItemId);
            if (findItem) ErrorException(L("ComponentsCannotBeUseToBuildItsOwnItem"));

            if (input.Type == BOMType.StandardPackaging)
            {
                if (input.BOMItems.Count > 1) MoreThanException(L("StandardPackageItems"), "1");
            }
            else
            {
                var checkDuplicate = input.BOMItems.GroupBy(s => s.ItemId).Any(s => s.Count() > 1);
                if (checkDuplicate) DuplicateException(L("BOMItems"));
            }
        }

        protected override async Task ValidateInputAsync(BOM input)
        {  
            await base.ValidateInputAsync(input);

            var itemIds = input.BOMItems.Select(s => s.ItemId).Distinct().ToList();
            itemIds.Add(input.ItemId);

            var validItem = await _repository.GetAll().AsNoTracking().Where(s => itemIds.Contains(s.Id)).CountAsync() == itemIds.Count;

            if (!validItem) InvalidException(L("Item"));
        }

        protected override BOM CreateInstance(BOM input)
        {
            return BOM.Create(input.TenantId, input.CreatorUserId.Value, input.Name, input.DisplayName, input.Type, input.ItemId);
        }
        protected override void UpdateInstance(BOM input, BOM entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.Name, input.DisplayName, input.Type, input.ItemId);
        }

        #endregion


        public override async Task<IdentityResult> InsertAsync(BOM input)
        {
            var result = await base.InsertAsync(input);

            await CurrentUnitOfWork.SaveChangesAsync();
            var bomItems = input.BOMItems.Select(s => BOMItem.Create(input.TenantId, input.CreatorUserId.Value, input.Id, s.ItemId, s.Qty)).ToList();
            await _bomItemRepository.BulkInsertAsync(bomItems);

            return result;
        }

        protected override async Task BeforeInstanceUpdateAsync(BOM input, BOM entity)
        {
            var bomItems = await _bomItemRepository.GetAll().AsNoTracking().Where(s => s.BOMId == input.Id).ToListAsync();

            var addBomItems = new List<BOMItem>();
            var updateBomItems = new List<BOMItem>();

            foreach (var item in input.BOMItems)
            {
                var updateBomItem = bomItems.FirstOrDefault(s => s.ItemId == item.ItemId);
                if (updateBomItem != null)
                {
                    updateBomItem.Update(input.LastModifierUserId.Value, item.ItemId, item.Qty);
                    updateBomItems.Add(updateBomItem);
                }
                else
                {
                    var newBomItem = BOMItem.Create(input.TenantId, input.CreatorUserId.Value, input.Id, item.ItemId, item.Qty);
                    addBomItems.Add(newBomItem);
                }
            }

            if (addBomItems.Any()) await _bomItemRepository.BulkInsertAsync(addBomItems);
            if (updateBomItems.Any()) await _bomItemRepository.BulkUpdateAsync(updateBomItems);

            var deleteBomItems = bomItems.Where(s => !updateBomItems.Any(r => r.Id == s.Id)).ToList();
            if (deleteBomItems.Any()) await _bomItemRepository.BulkDeleteAsync(deleteBomItems);

        }

        protected override async Task BeforeInstanceDeleteAsync(BOM entity)
        {
            var bomItems = await _bomItemRepository.GetAll().AsNoTracking().Where(s => s.BOMId == entity.Id).ToListAsync();
            await _bomItemRepository.BulkDeleteAsync(bomItems);
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var inputFile = new ExportFileInput
            {
                FileName = $"{InstanceKeyName}.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Name_",InstanceName), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Type"), Width = 250 },
                    new ColumnOutput{ ColumnTitle = L("Item"), Width = 250 },
                    new ColumnOutput{ ColumnTitle = L("Default"), Width = 150 },
                }
            };

            return await _excelManager.ExportExcelTemplateAsync(inputFile);
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
            var entities = new List<BOM>();
            var entityHash = new HashSet<string>();
            var codeHash = new HashSet<string>();
          
            
            var excelPackage = await _fileStorageManager.DownloadExcel(input.Token);
            if (excelPackage != null)
            {
                // Get the work book in the file
                var workBook = excelPackage.Workbook;
                if (workBook != null)
                {
                    // retrive first worksheets
                    var worksheet = workBook.Worksheets[0];
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var rowInfo = $", Row = {i}";

                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, rowInfo);
                        if (entityHash.Contains(name)) DuplicateNameException(name, rowInfo);

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, rowInfo);

                        BOMType type = BOMType.StandardPackaging;

                        Guid itemId = Guid.NewGuid();

                        var code = worksheet.GetString(i, 3);
                        ValidateCodeInput(code, rowInfo);
                        if (code.Length > BiiSoftConsts.MaxLengthItemFieldCode) MoreThanCharactersException(L("Code_", InstanceName), BiiSoftConsts.MaxLengthItemFieldCode, rowInfo);
                        if (codeHash.Contains(code)) DuplicateCodeException(code, rowInfo);
                        codeHash.Add(code);                       

                        var isDefault = worksheet.GetBool(i, 4);

                        var entity = BOM.Create(input.TenantId.Value, input.UserId.Value, name, displayName, type, itemId);
                        entity.SetDefault(isDefault);

                        entities.Add(entity);
                        entityHash.Add(name);
                    }
                }
            }

            if (!entities.Any()) return IdentityResult.Success;

            var updateColorPatternDic = new Dictionary<string, BOM>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    updateColorPatternDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => entityHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
                }
            }

            var addColorPatterns = new List<BOM>();

            foreach (var l in entities)
            {
                if (updateColorPatternDic.ContainsKey(l.Name))
                {
                    updateColorPatternDic[l.Name].Update(input.UserId.Value, l.Name, l.DisplayName, l.Type, l.ItemId);
                    updateColorPatternDic[l.Name].SetDefault(l.IsDefault);
                }
                else
                {
                    addColorPatterns.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    if (updateColorPatternDic.Any()) await _repository.BulkUpdateAsync(updateColorPatternDic.Values.ToList());
                    if (addColorPatterns.Any()) await _repository.BulkInsertAsync(addColorPatterns);
                }
                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
