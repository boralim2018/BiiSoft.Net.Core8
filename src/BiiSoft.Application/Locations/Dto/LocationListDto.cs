using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Locations.Dto
{
    public class LocationListDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
