using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Currencies.Dto
{
    public class FindCurrencyDto : DefaultNameDto<long>
    {      
        public string Code { get; set; }
        public string Symbol { get; set; }
    }
}
