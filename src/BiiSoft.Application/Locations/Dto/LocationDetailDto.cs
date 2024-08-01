using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Locations.Dto
{
    public class LocationDetailDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public Guid? FirstId { get; set; }
        public Guid? NextId { get; set; }
        public Guid? PreviousId { get; set; }
        public Guid? LastId { get; set; }
    }
}
