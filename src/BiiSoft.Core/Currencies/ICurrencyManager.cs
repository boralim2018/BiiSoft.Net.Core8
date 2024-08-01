using Abp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public interface ICurrencyManager : IDefaultActiveValidateServiceBase<Currency, long>, IImporxExcelValidateSerivceBase<long>
    {

    }
   
}
