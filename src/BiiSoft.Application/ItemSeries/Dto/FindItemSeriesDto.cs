using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Items.Series.Dto
{
    public class FindItemSeriesDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
