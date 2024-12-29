using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Items.Series.Dto
{
    public class ItemSeriesListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
