using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemBrands
{
    public interface IItemBrandManager : IDefaultActiveValidateServiceBase<ItemBrand, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
