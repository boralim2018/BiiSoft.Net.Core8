using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ColorPatterns.Dto
{
    public class FindColorPatternDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
