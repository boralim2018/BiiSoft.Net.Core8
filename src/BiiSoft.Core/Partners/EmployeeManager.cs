using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IRepository<Employee, Guid> _repository;
        public EmployeeManager(IRepository<Employee, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(Employee @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(Employee @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(Employee @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> EnableAsync(Employee @entity)
        {
            @entity.Enable(true);
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DisableAsync(Employee @entity)
        {
            entity.Enable(false);
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
