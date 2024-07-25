using Abp.Timing;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.FileStorages;
using BiiSoft.Extensions;

namespace BiiSoft.Currencies
{
    public class CurrencyManager : BiiSoftDefaultNameActiveValidateServiceBase<Currency, long>, ICurrencyManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public CurrencyManager(
            IBiiSoftRepository<Currency, long> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager): base(repository) 
        {
            _fileStorageManager=fileStorageManager;
            _unitOfWorkManager=unitOfWorkManager;
        }
        
        #region override base class
        protected override string InstanceName => L("Currency");

        protected override void ValidateInput(Currency input)
        {
            ValidateCodeInput(input.Code);
            base.ValidateInput(input);
            ValidateInput(input.Symbol, L("Symbol"));
        }

        protected override async Task ValidateInputAsync(Currency input)
        {
            ValidateInput(input);

            bool find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.Code == input.Code);
            if (find) DuplicateCodeException(input.Code);
        }


        protected override Currency CreateInstance(int? tenantId, long userId, Currency input)
        {
            return Currency.Create(userId, input.Name, input.DisplayName, input.Code, input.Symbol);
        }

        protected override void UpdateInstance(long userId, Currency input, Currency entity)
        {
            entity.Update(userId, input.Name, input.DisplayName, input.Code, input.Symbol);
        }

        #endregion


        /// <summary>
        /// Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportAsync(long userId, string fileToken)
        {  
            var currencys = new List<Currency>();
            var currencyHash = new HashSet<string>();
            var defaultCode = "";

            //var excelPackage = Read(input, _appFolders);
            var excelPackage = await _fileStorageManager.DownloadExcel(fileToken);
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
                        if (currencyHash.Contains(code)) DuplicateCodeException(code, $", Row = {i}");
                        
                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, $", Row = {i}");

                        var symbol = worksheet.GetString(i, 4);

                        var isDefault = worksheet.GetBool(i, 5);
                        if (isDefault && defaultCode != "") MoreThanException(L("Default"), 1.ToString(), $", Row = {i}");
                        else if (isDefault) defaultCode = code;

                        var entity = Currency.Create(userId, name, displayName, code, symbol??code);
                        if(isDefault) entity.SetDefault(isDefault);

                        currencys.Add(entity);
                        currencyHash.Add(code);
                    }
                }
            }

            if (!currencys.Any()) return IdentityResult.Success;

            var updateCurrencyDic = new Dictionary<string, Currency>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if(defaultCode != "")
                {
                    bool otherDefault = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.IsDefault && s.Code != defaultCode);
                    if (otherDefault) throw MoreThanException(L("Default"), 1.ToString());
                }

                updateCurrencyDic  = await _repository.GetAll().AsNoTracking()
                                           .Where(s => currencyHash.Contains(s.Code))
                                           .ToDictionaryAsync(k => k.Code, v => v);
            }

            var addCurrencies = new List<Currency>();
            foreach (var i in currencys)
            {   
                if (updateCurrencyDic.ContainsKey(i.Code))
                {
                    updateCurrencyDic[i.Code].Update(userId, i.Name, i.DisplayName, i.Code, i.Symbol);
                    updateCurrencyDic[i.Code].SetDefault(i.IsDefault);
                }
                else
                {
                    addCurrencies.Add(i);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                if (updateCurrencyDic.Any()) await _repository.BulkUpdateAsync(updateCurrencyDic.Values.ToList());
                if (addCurrencies.Any()) await _repository.BulkInsertAsync(addCurrencies);

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }

    }
}
