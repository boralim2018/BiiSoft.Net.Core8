using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Classes
{
    public class ClassManager : IClassManager
    {
        private readonly IRepository<Class, Guid> _repository;
        public ClassManager(IRepository<Class, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(Class @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<Class> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(Class @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(Class @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
