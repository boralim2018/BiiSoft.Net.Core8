using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class CompanySettingManager : ICompanySettingManager
    {
        private readonly IRepository<CompanyGeneralSetting, long> _repository;
        public CompanySettingManager(IRepository<CompanyGeneralSetting, long> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(CompanyGeneralSetting @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<CompanyGeneralSetting> GetAsync(long id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CompanyGeneralSetting> GetCompanySettingAsync()
        {
            return await _repository.GetAll().FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> RemoveAsync(CompanyGeneralSetting @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(CompanyGeneralSetting @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
