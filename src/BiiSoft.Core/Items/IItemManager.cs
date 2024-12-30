using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Items
{
    public interface IItemManager : IActiveValidateServiceBase<Item, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
  
    }
   
}
