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
using BiiSoft.Excels;

namespace BiiSoft.Taxes
{
    public class TaxManager : BiiSoftDefaultNameActiveValidateServiceBase<Tax, Guid>, ITaxManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<ChartOfAccount, Guid> _chartOfAccountRepository;
        private readonly IAppFolders _appFolders;
        private readonly IExcelManager _excelManager;
        public TaxManager(
            IExcelManager excelManager,
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ChartOfAccount, Guid> chartOfAccountRepository,
            IBiiSoftRepository<Tax, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _chartOfAccountRepository = chartOfAccountRepository;
            _appFolders = appFolders;
            _excelManager = excelManager;
        }

        #region override
        protected override string InstanceName => L("Tax");
        protected override bool IsUniqueName => true;

        protected override void ValidateInput(Tax input)
        {
            base.ValidateInput(input);

            ValidateSelect(input.PurchaseAccountId, L("PurchaseAccount"));
            ValidateSelect(input.SaleAccountId, L("SaleAccount"));
        }

        protected override async Task ValidateInputAsync(Tax input)
        {
            await base.ValidateInputAsync(input);

            var validatePurchaseAccount = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.PurchaseAccountId.Value);
            if (!validatePurchaseAccount) InvalidException(L("PurchaseAccount"));
          
            var validateSaleAccount = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.SaleAccountId.Value);
            if (!validateSaleAccount) InvalidException(L("SaleAccount"));
        }

        protected override Tax CreateInstance(Tax input)
        {
            return Tax.Create(input.TenantId, input.CreatorUserId.Value, input.Name, input.DisplayName, input.Rate, input.PurchaseAccountId, input.SaleAccountId);
        }

        protected override void UpdateInstance(Tax input, Tax entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.Name, input.DisplayName, input.Rate, input.PurchaseAccountId, input.SaleAccountId);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"Tax.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Tax")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Rate"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Purchase"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("SaleAccount"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Default"), Width = 150 },
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
            var taxes = new List<Tax>();
            var taxHash = new HashSet<string>();
            var accountDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    accountDic = await _chartOfAccountRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                }
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
                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, $", Row = {i}");
                        if (taxHash.Contains(name)) DuplicateCodeException(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        decimal rate = worksheet.GetDecimal(i, 3);

                        Guid? purchaseAccountId = null;
                        var purchaseAccount = worksheet.GetString(i, 4);
                        ValidateInput(purchaseAccount, L("PurchaseAccount"), $", Row = {i}");
                        if (!accountDic.ContainsKey(purchaseAccount)) InvalidException(L("PurchaseAccount"), $", Row = {i}");
                        purchaseAccountId = accountDic[purchaseAccount];

                        Guid? saleAccountId = null;
                        var saleAccount = worksheet.GetString(i, 5);
                        ValidateInput(saleAccount, L("SaleAccount"), $", Row = {i}");
                        if (!accountDic.ContainsKey(saleAccount)) InvalidException(L("SaleAccount"), $", Row = {i}");
                        saleAccountId = accountDic[saleAccount];

                        var isDefault = worksheet.GetBool(i, 6);
                        var cannotEdit = worksheet.GetBool(i, 7);
                        var cannotDelete = worksheet.GetBool(i, 8); 

                        var entity = Tax.Create(input.TenantId, input.UserId.Value, name, displayName, rate, purchaseAccountId, saleAccountId);
                        entity.SetDefault(isDefault);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        taxes.Add(entity);
                        taxHash.Add(name);
                    }
                }
            }

            if (!taxes.Any()) return IdentityResult.Success;

            var updateTaxDic = new Dictionary<string, Tax>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    updateTaxDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => taxHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
                }
            }

            var addTaxs = new List<Tax>();

            foreach (var l in taxes)
            {
                if (updateTaxDic.ContainsKey(l.Name))
                {
                    updateTaxDic[l.Name].Update(input.UserId.Value, l.Name, l.DisplayName, l.Rate, l.PurchaseAccountId, l.SaleAccountId);
                    updateTaxDic[l.Name].SetDefault(l.IsDefault);
                    updateTaxDic[l.Name].SetCannotEdit(l.CannotEdit);
                    updateTaxDic[l.Name].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addTaxs.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    if (updateTaxDic.Any()) await _repository.BulkUpdateAsync(updateTaxDic.Values.ToList());
                    if (addTaxs.Any()) await _repository.BulkInsertAsync(addTaxs);
                }
                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
