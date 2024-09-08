using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Currencies.Dto
{
    public class CurrencyDetailDto : DefaultNameActiveAuditedNavigationDto<long>
    {      
        public string Code { get; set; }
        public string Symbol { get; set; }

    }
}
