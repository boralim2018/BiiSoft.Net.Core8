using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Taxes.Dto
{
    public class FindTaxDto : NameActiveDto<Guid>
    {
        public decimal Rate { get; set; }
    }
}
