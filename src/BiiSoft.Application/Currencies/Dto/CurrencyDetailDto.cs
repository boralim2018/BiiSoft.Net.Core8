using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Currencies.Dto
{
    public class CurrencyDetailDto : DefaultNameActiveAuditedDto<long>
    {      
        public string Code { get; set; }
        public string Symbol { get; set; }

        public long? FirstId { get; set; }
        public long? NextId { get; set; }
        public long? PreviousId { get; set; }
        public long? LastId { get; set; }
    }
}
