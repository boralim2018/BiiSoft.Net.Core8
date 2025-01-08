using Abp.Dependency;
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

namespace BiiSoft.Items
{
    public abstract class ItemFieldManagerBase<TEntity> : BiiSoftDefaultNameActiveValidateServiceBase<TEntity, Guid>, IItemFieldManagerBase<TEntity> where TEntity : ItemFieldBase
    {
        protected readonly IFileStorageManager _fileStorageManager;
        protected readonly IUnitOfWorkManager _unitOfWorkManager;
        protected readonly IExcelManager _excelManager;

        private readonly IBiiSoftRepository<ItemFieldSetting, Guid> _itemFieldSettingRepository;
        
        public ItemFieldManagerBase(
            IBiiSoftRepository<TEntity, Guid> repository) : base(repository) 
        {
            _fileStorageManager = IocManager.Instance.Resolve<IFileStorageManager>();
            _unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
            _excelManager = IocManager.Instance.Resolve<IExcelManager>();
            _itemFieldSettingRepository = IocManager.Instance.Resolve<IBiiSoftRepository<ItemFieldSetting, Guid>>();
        }

        #region override
        protected abstract string InstanceKeyName { get; }
        protected override string InstanceName => L(InstanceKeyName);
        protected override bool IsUniqueName => true;

        protected abstract TEntity CreateInstance(int tenantId, long userId, string name, string displayName, string code);

        private async Task<bool> CheckUseCodeAsync()
        {
            return await _itemFieldSettingRepository.GetAll().AsNoTracking().AnyAsync(s => s.UseCode);
        }

        protected override async Task ValidateInputAsync(TEntity input)
        {
            var useCode = await CheckUseCodeAsync();
            if (useCode)
            {
                ValidateCodeInput(input.Code);

                var findCode = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.Code == input.Code);
                if (findCode) DuplicateCodeException(input.Code);
            }

            await base.ValidateInputAsync(input);
        }

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
                    new ColumnOutput{ ColumnTitle = L("Name_",InstanceName), Width = 250, IsRequired = true },
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
            var codeHash = new HashSet<string>();
            bool useCode = false;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    useCode = await CheckUseCodeAsync();
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
                        var rowInfo = $", Row = {i}";

                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, rowInfo);
                        if (entityHash.Contains(name)) DuplicateNameException(name, rowInfo);

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, rowInfo);

                        var code = worksheet.GetString(i, 3);
                        if (useCode)
                        {
                            ValidateCodeInput(code, rowInfo);
                            if (codeHash.Contains(code)) DuplicateCodeException(code, rowInfo);

                            codeHash.Add(code);
                        }

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
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    updateColorPatternDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => entityHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
                }
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
