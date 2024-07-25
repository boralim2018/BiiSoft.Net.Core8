using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Currencies.Dto
{
    public class CurrencyListDto : DefaultNameActiveAuditedDto<long>
    {      
        public string Code { get; set; }
        public string Symbol { get; set; }
    }
}
