using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.VGAs.Dto
{
    public class FindVGADto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
