using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Screens.Dto
{
    public class FindScreenDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
