using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.BFiles.Dto;
using BiiSoft.Dtos;
using BiiSoft.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft
{
    public interface IBiiSoftValidateServiceBase<TEntity, TPrimaryKey> : IDomainService where TPrimaryKey : struct
    {
        Task<TEntity> FindAsync(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<IdentityResult> InsertAsync(TEntity input);
        Task<IdentityResult> UpdateAsync(TEntity input);
        Task<IdentityResult> DeleteAsync(TPrimaryKey id);
        Task MapNavigation(INavigationDto<TPrimaryKey> result);
    }
    
    public interface IActiveValidateServiceBase<TEntity, TPrimaryKey> : IBiiSoftValidateServiceBase<TEntity, TPrimaryKey> where TPrimaryKey : struct
    {
       
        Task<IdentityResult> EnableAsync(IUserEntity<TPrimaryKey> input);
        Task<IdentityResult> DisableAsync(IUserEntity<TPrimaryKey> input);
    }

    public interface IDefaultActiveValidateServiceBase<TEntity, TPrimaryKey> : IActiveValidateServiceBase<TEntity, TPrimaryKey> where TPrimaryKey : struct
    {
        Task<IdentityResult> SetAsDefaultAsync(IUserEntity<TPrimaryKey> input);
        Task<IdentityResult> UnsetAsDefaultAsync(IUserEntity<TPrimaryKey> input);
        Task<TEntity> GetDefaultValueAsync();
    }


    public interface IImporxExcelValidateSerivceBase<TPrimaryKey> where TPrimaryKey : struct
    {
        Task<ExportFileOutput> ExportExcelTemplateAsync();
        Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<TPrimaryKey> input);
    }
}
