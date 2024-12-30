using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemGroups.Dto
{
    public class FindItemGroupDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
