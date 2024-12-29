using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.VGAs
{
    public interface IVGAManager : IDefaultActiveValidateServiceBase<VGA, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
