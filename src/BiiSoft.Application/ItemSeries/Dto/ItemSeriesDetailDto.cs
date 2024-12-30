using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Items.Series.Dto
{
    public class ItemSeriesDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
