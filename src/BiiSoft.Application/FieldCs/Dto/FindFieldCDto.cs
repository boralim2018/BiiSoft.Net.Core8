using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldCs.Dto
{
    public class FindFieldCDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
