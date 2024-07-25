using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public class PartnerManager : IPartnerManager
    {
        private readonly IRepository<Partner, Guid> _repository;
        public PartnerManager(IRepository<Partner, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(Partner @entity)
        {
            await _repository.InsertAsync(@entity);            
            return IdentityResult.Success;
        }

        public async Task<Partner> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(Partner @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(Partner @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> EnableAsync(Partner @entity)
        {
            @entity.Enable(true);
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DisableAsync(Partner @entity)
        {
            entity.Enable(false);
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
