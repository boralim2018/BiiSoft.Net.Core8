using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public class PartnerGroupManager : IPartnerGroupManager
    {
        private readonly IRepository<PartnerGroup, Guid> _repository;
        public PartnerGroupManager(IRepository<PartnerGroup, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(PartnerGroup @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<PartnerGroup> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(PartnerGroup @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(PartnerGroup @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
