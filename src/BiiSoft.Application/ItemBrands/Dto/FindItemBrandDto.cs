using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemBrands.Dto
{
    public class FindItemBrandDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
