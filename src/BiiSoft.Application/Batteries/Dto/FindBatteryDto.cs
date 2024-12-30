using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Batteries.Dto
{
    public class FindBatteryDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
