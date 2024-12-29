using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.HDDs
{
    public interface IHDDManager : IDefaultActiveValidateServiceBase<HDD, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
