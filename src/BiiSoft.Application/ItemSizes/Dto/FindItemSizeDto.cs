using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemSizes.Dto
{
    public class FindItemSizeDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
