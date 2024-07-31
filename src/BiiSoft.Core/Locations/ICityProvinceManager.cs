using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface ICityProvinceManager : IDomainService
    {
        Task<CityProvince> FindAsync(IEntity<Guid> innput);
        Task<CityProvince> GetAsync(IEntity<Guid> input);
        Task<IdentityResult> InsertAsync(long userId, CityProvince input);
        Task<IdentityResult> UpdateAsync(long userId, CityProvince input);
        Task<IdentityResult> DeleteAsync(IEntity<Guid> innput);
        Task<IdentityResult> EnableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> DisableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> ImportAsync(long userId, string fileToken);
    }
   
}
