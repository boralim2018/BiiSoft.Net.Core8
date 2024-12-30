using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemModels.Dto
{
    public class FindItemModelDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
