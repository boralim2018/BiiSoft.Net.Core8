using Abp.Domain.Services;
using BiiSoft.Branches;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo
{
    public interface IContactAddressBaseManager<TEntity, TPrimaryKey> : IDomainService where TEntity : ContactAddressBase<TPrimaryKey>
    {
        Task<TEntity> GetAsync(TPrimaryKey id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, TEntity input);
        Task<IdentityResult> UpdateAsync(long userId, TEntity input);
        Task<IdentityResult> DeleteAsync(TPrimaryKey id);
        Task<IdentityResult> ChangeLocationAsync(long userId, Guid? locationId, TPrimaryKey id);

        Task BulkValidateAsync(List<TEntity> input);
        Task<IdentityResult> BulkInsertAsync(int? tenantId, long userId, List<TEntity> input);
        Task<IdentityResult> BulkUpdateAsync(long userId, List<TEntity> input);
        Task<IdentityResult> BulkDeleteAsync(List<TPrimaryKey> input);
    }

}
