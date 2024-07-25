using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public class EmployeeContactAddressManager : IEmployeeContactAddressManager
    {
        private readonly IRepository<EmployeeContactAddress, Guid> _repository;
        public EmployeeContactAddressManager(IRepository<EmployeeContactAddress, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(EmployeeContactAddress @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<EmployeeContactAddress> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(EmployeeContactAddress @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(EmployeeContactAddress @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
