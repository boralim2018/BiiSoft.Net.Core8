using BiiSoft.Branches;
using BiiSoft.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo
{
    public interface ITransactionNoSettingManager : IBiiSoftValidateServiceBase<TransactionNoSetting, Guid>
    {
        Task BulkValidateAsync(List<TransactionNoSetting> input);
        Task<IdentityResult> BulkInsertAsync(IMayHaveTenantBulkInputEntity<TransactionNoSetting> input);
        Task<IdentityResult> BulkUpdateAsync(IBulkInputIntity<TransactionNoSetting> input);
        Task<IdentityResult> BulkDeleteAsync(List<Guid> input);
    }

}
