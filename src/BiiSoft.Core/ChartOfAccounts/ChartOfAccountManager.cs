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
using BiiSoft.Branches;

namespace BiiSoft.ChartOfAccounts
{
    public class ChartOfAccountManager : BiiSoftNameActiveValidateServiceBase<ChartOfAccount, Guid>, IChartOfAccountManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAppFolders _appFolders;
        private readonly IBiiSoftRepository<CompanyAdvanceSetting, long> _companyAdvanceSettingRepository;
        
        public ChartOfAccountManager(
            IAppFolders appFolders,
            IBiiSoftRepository<ChartOfAccount, Guid> repository,
            IBiiSoftRepository<CompanyAdvanceSetting, long> companyAdvanceSettingRepository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager): base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _appFolders = appFolders;
            _companyAdvanceSettingRepository = companyAdvanceSettingRepository;
        }

        #region override base class
      
        protected override string InstanceName => L("ChartOfAccount");
        protected override bool IsUniqueName => true;

        protected override void ValidateInput(ChartOfAccount input)
        {
            ValidateCodeInput(input.Code);
            base.ValidateInput(input);

            if (input.AccountType != input.SubAccountType.Parent()) InvalidException(L("AccountType"));
        }

        protected override async Task ValidateInputAsync(ChartOfAccount input)
        {
            await base.ValidateInputAsync(input);

            var findCode = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Code == input.Code && s.Id != input.Id);
            if (findCode) DuplicateCodeException(input.Code);

            if (!input.ParentId.IsNullOrEmpty())
            {
                var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ParentId);
                if (!find) InvalidException(L("ParentAccount"));
            }
        }

        protected override ChartOfAccount CreateInstance(ChartOfAccount input)
        {
            return ChartOfAccount.Create(input.TenantId, input.CreatorUserId.Value, input.SubAccountType, input.Code, input.Name, input.DisplayName, input.ParentId);
        }

        private async Task<bool> CheckAutoGenerateCodeAsync()
        {
            return !await _companyAdvanceSettingRepository.GetAll().AsNoTracking().Select(s => s.CustomAccountCodeEnable).FirstOrDefaultAsync();
        }

        protected override async Task BeforeInstanceUpdate(ChartOfAccount input, ChartOfAccount entity)
        {
            var autoGenerateCode = await CheckAutoGenerateCodeAsync();

            if (autoGenerateCode && entity.SubAccountType != input.SubAccountType) await SetCodeAsync(input);
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
                input.SetCode(0.GenerateCode(BiiSoftConsts.ChartOfAccountCodeLength, prefix));
            }
            else
            {
                input.SetCode(latestCode.NextCode(prefix));
            }
        }

        public override async Task<IdentityResult> InsertAsync(ChartOfAccount input)
        {
            var autoGenerateCode = await CheckAutoGenerateCodeAsync();
            if (autoGenerateCode) await SetCodeAsync(input);
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
            var accounts = new List<ChartOfAccount>();
            var autoGenerateCode = false;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    accounts = await _repository.GetAll().AsNoTracking().ToListAsync();
                    autoGenerateCode = await CheckAutoGenerateCodeAsync();
                }
            }

            var addAccounts = new List<ChartOfAccount>();
            var latestCodeDic = accounts.GroupBy(s => s.SubAccountType)
                                        .ToDictionary(k => k.Key, v => v.MaxBy(m => m.Code).Code);

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
                        if (!autoGenerateCode) ValidateCodeInput(code, $", Row: {i}");
                       
                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row: {i}");

                        var findName = accounts.Any(a => a.Name == name);
                        if(findName) DuplicateNameException(name, $", Row: {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row: {i}");

                        var findDisplayName = accounts.Any(a => a.DisplayName == displayName);
                        if (findDisplayName) DuplicateNameException(displayName, $", Row: {i}");

                        var subtAccount = worksheet.GetString(i, 4)?.Replace(" ", "");
                        ValidateInput(subtAccount, L("SubAccountType"), $", Row: {i}");
                        SubAccountType subAccountType = (SubAccountType)Enum.Parse(typeof(SubAccountType), subtAccount);

                        Guid? parentId = null;
                        var parent = worksheet.GetString(i, 5);
                        if (!parent.IsNullOrWhiteSpace())
                        {
                            var find = accounts.FirstOrDefault(a => a.Name.ToLower() == parent.ToLower().Trim());
                            if (find != null) InvalidException(L("ParentAccount"), $", Row: {i}");

                            parentId = find.Id;
                        }
                       
                        var cannotEdit = worksheet.GetBool(i, 6);
                        var cannotDelete = worksheet.GetBool(i, 7);

                        if (autoGenerateCode)
                        {
                            if (code.IsNullOrWhiteSpace())
                            {
                                if (latestCodeDic.ContainsKey(subAccountType))
                                {
                                    code = latestCodeDic[subAccountType].NextCode(subAccountType.ToIntStr());
                                }
                                else
                                {
                                    code = 0.GenerateCode(BiiSoftConsts.ChartOfAccountCodeLength, subAccountType.ToIntStr());
                                }
                            }
                            else 
                            {
                                if (!code.IsCode(BiiSoftConsts.ChartOfAccountCodeLength, subAccountType.ToIntStr())) InvalidCodeException(code, $", Row: {i}");
                            }

                            if (!latestCodeDic.ContainsKey(subAccountType))
                            {
                                latestCodeDic.Add(subAccountType, code);
                            }
                            else if (code.CompareTo(latestCodeDic[subAccountType]) > 0)
                            {
                                 latestCodeDic[subAccountType] = code;
                            }
                        }

                        var findCode = accounts.Any(a => a.Code == code);
                        if (findCode) DuplicateCodeException(code, $", Row: {i}");

                        var entity = ChartOfAccount.Create(input.TenantId.Value, input.UserId.Value, subAccountType, code, name, displayName, parentId);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        addAccounts.Add(entity);
                        accounts.Add(entity);
                    }
                }
            }

            if (!addAccounts.Any()) return IdentityResult.Success;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                   await _repository.BulkInsertAsync(addAccounts);
                }

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
        
    }
}
