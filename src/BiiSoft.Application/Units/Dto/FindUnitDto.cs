using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Units.Dto
{
    public class FindUnitDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
