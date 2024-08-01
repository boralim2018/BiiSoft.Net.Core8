using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface ICountryManager : IActiveValidateServiceBase<Country, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
        
    }
   
}
