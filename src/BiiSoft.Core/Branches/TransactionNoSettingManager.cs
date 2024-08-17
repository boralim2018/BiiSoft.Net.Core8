using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Branches;

namespace BiiSoft.ContactInfo
{
    public class TransactionNoSettingManager : BiiSoftValidateServiceBase<TransactionNoSetting, long>, ITransactionNoSettingManager
    {

        public TransactionNoSettingManager(
            IBiiSoftRepository<TransactionNoSetting, long> repository) : base(repository)
        {
     
        }

        protected override string InstanceName => L("TransactionNo");

        protected override TransactionNoSetting CreateInstance(TransactionNoSetting input)
        {
            return TransactionNoSetting.Create(input.TenantId, input.CreatorUserId, input.JournalType, input.CustomTransactionNoEnable, input.Prefix, input.Digits, input.Start, input.RequiredReference);
        }

        protected override void UpdateInstance(TransactionNoSetting input, TransactionNoSetting entity)
        {
            entity.Update(input.LastModifierUserId, input.JournalType, input.CustomTransactionNoEnable, input.Prefix, input.Digits, input.Start, input.RequiredReference);
        }

        protected override async Task ValidateInputAsync(TransactionNoSetting input)
        {   
            if (input.CustomTransactionNoEnable) return;

            if (input.Digits <= 0) MustBeGreaterThanException(L("Digits"), 0);
            if (input.Start <= 0) MustBeGreaterThanException(L("Start"), 0);

            var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.JournalType == input.JournalType);
            if (find) DuplicateException(InstanceName, input.JournalType.ToString());
        }

        protected virtual void BulkValidate(List<TransactionNoSetting> input)
        {
            var validateInputs = input.Where(s => !s.CustomTransactionNoEnable).ToList();
            if (!validateInputs.Any()) return;

            var validateDigit = validateInputs.Where(s => s.Digits <= 0).FirstOrDefault();
            if(validateDigit != null) MustBeGreaterThanException(L("Digits"), 0 , validateDigit.JournalType.ToString());

            var validateStart = validateInputs.Where(s => s.Start <= 0).FirstOrDefault();
            if (validateStart != null) MustBeGreaterThanException(L("Start"), 0, validateDigit.JournalType.ToString());
        }

        public async virtual Task BulkValidateAsync(List<TransactionNoSetting> input)
        {
            BulkValidate(input);

            var find = await _repository.GetAll().AsNoTracking().Where(s => input.Any(i => i.Id != s.Id && i.JournalType == s.JournalType)).FirstOrDefaultAsync();
            if (find != null) DuplicateException(InstanceName, find.JournalType.ToString());
        }

        public async Task<IdentityResult> BulkInsertAsync(IMayHaveTenantBulkInputEntity<TransactionNoSetting> input)
        {
            await BulkValidateAsync(input.Items);

            var entities = input.Items.Select(s => {
                s.TenantId = input.TenantId.Value;
                s.CreatorUserId = input.UserId;

                var e = CreateInstance(s);
                s.Id = e.Id;

                return e;
            }).ToList();

            await _repository.BulkInsertAsync(entities);          
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkUpdateAsync(IBulkInputIntity<TransactionNoSetting> input)
        {
            await BulkValidateAsync(input.Items);

            var entities = await _repository.GetAll().AsNoTracking().Where(s => input.Items.Any(r => r.Id.Equals(s.Id))).ToDictionaryAsync(k => k.Id, v => v);

            if (entities.Count != input.Items.Count) NotFoundException(InstanceName);

            foreach (var i in input.Items)
            {
                if (!entities.ContainsKey(i.Id)) NotFoundException(InstanceName);

                i.LastModifierUserId = input.UserId;
                UpdateInstance(i, entities[i.Id]);
            }

            await _repository.BulkUpdateAsync(entities.Values.ToList());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkDeleteAsync(List<long> input)
        {
            var entities = await _repository.GetAll().AsNoTracking().Where(s => input.Contains(s.Id)).ToListAsync();

            if(entities.Count != input.Count) NotFoundException(InstanceName);

            if (entities.Any()) await _repository.BulkDeleteAsync(entities);
          
            return IdentityResult.Success;
        }

    }
}
