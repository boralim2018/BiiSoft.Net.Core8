using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface IKhanDistrictManager : IActiveValidateServiceBase<KhanDistrict, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
