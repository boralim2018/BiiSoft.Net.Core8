using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface IVillageManager : IActiveValidateServiceBase<Village, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
        
    } 
   
}
