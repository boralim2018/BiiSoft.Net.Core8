using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public class PartnerContactPersonManager : IPartnerContactPersonManager
    {
        private readonly IRepository<PartnerContactPerson, Guid> _repository;
        public PartnerContactPersonManager(IRepository<PartnerContactPerson, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(PartnerContactPerson @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<PartnerContactPerson> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(PartnerContactPerson @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(PartnerContactPerson @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
