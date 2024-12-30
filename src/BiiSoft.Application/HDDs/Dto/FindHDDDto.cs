using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.HDDs.Dto
{
    public class FindHDDDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
