using System;
using BiiSoft.BFiles.Dto;
using BiiSoft.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BiiSoft.Items
{
    public interface IItemManager : IActiveValidateServiceBase<Item, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
        Task<ExportFileOutput> ExportExcelUpdateItemZonesAsync();
        Task<IdentityResult> ImportExcelUpdateItemZonesAsync(IImportExcelEntity<Guid> input);
    }
   
}
