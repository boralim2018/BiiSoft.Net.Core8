﻿using Abp.Domain.Uow;
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
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;
using BiiSoft.Excels;
using Abp.Dependency;

namespace BiiSoft.Items
{
    public abstract class ItemFieldManagerBase<TEntity> : BiiSoftDefaultNameActiveValidateServiceBase<TEntity, Guid>, IItemFieldManagerBase<TEntity> where TEntity : ItemFieldBase
    {
        protected readonly IFileStorageManager _fileStorageManager;
        protected readonly IUnitOfWorkManager _unitOfWorkManager;
        protected readonly IAppFolders _appFolders;
        protected readonly IExcelManager _excelManager;
        public ItemFieldManagerBase(
            IBiiSoftRepository<TEntity, Guid> repository) : base(repository) 
        {
            _fileStorageManager = IocManager.Instance.Resolve<IFileStorageManager>();
            _unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
            _appFolders = IocManager.Instance.Resolve<IAppFolders>();
            _excelManager = IocManager.Instance.Resolve<IExcelManager>();
        }

        #region override
        protected abstract string InstanceKeyName { get; }
        protected override string InstanceName => L(InstanceKeyName);
        protected override bool IsUniqueName => true;

        protected abstract TEntity CreateInstance(int tenantId, long userId, string name, string displayName, string code);

        protected override TEntity CreateInstance(TEntity input)
        {
            return CreateInstance(input.TenantId.Value, input.CreatorUserId.Value, input.Name, input.DisplayName, input.Code);
        }
        protected override void UpdateInstance(TEntity input, TEntity entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.Name, input.DisplayName, input.Code);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var inputFile = new ExportFileInput
            {
                FileName = $"{InstanceKeyName}.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Name_",L(InstanceKeyName)), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 250 },
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
            var entities = new List<TEntity>();
            var entityHash = new HashSet<string>();
           
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
                        if (entityHash.Contains(name)) DuplicateCodeException(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        var code = worksheet.GetString(i, 3);
                        var isDefault = worksheet.GetBool(i, 4);

                        var entity = CreateInstance(input.TenantId.Value, input.UserId.Value, name, displayName, code);
                        entity.SetDefault(isDefault);

                        entities.Add(entity);
                        entityHash.Add(name);
                    }
                }
            }

            if (!entities.Any()) return IdentityResult.Success;

            var updateColorPatternDic = new Dictionary<string, TEntity>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                updateColorPatternDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => entityHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
            }

            var addColorPatterns = new List<TEntity>();

            foreach (var l in entities)
            {
                if (updateColorPatternDic.ContainsKey(l.Name))
                {
                    updateColorPatternDic[l.Name].Update(input.UserId.Value, l.Name, l.DisplayName, l.Code);
                    updateColorPatternDic[l.Name].SetDefault(l.IsDefault);
                }
                else
                {
                    addColorPatterns.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateColorPatternDic.Any()) await _repository.BulkUpdateAsync(updateColorPatternDic.Values.ToList());
                if (addColorPatterns.Any()) await _repository.BulkInsertAsync(addColorPatterns);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
