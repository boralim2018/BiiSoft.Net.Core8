using BiiSoft.Dtos;
using System;

namespace BiiSoft.Items.Dto
{
    public class FindItemDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
