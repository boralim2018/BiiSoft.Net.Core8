using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldBs.Dto
{
    public class FindFieldBDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
