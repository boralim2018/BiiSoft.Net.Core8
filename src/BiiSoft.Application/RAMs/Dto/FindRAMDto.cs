using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.RAMs.Dto
{
    public class FindRAMDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
