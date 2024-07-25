using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public class PartnerContactAddressManager : IPartnerContactAddressManager
    {
        private readonly IRepository<PartnerContactAddress, Guid> _repository;
        public PartnerContactAddressManager(IRepository<PartnerContactAddress, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(PartnerContactAddress @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<PartnerContactAddress> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(PartnerContactAddress @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(PartnerContactAddress @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
