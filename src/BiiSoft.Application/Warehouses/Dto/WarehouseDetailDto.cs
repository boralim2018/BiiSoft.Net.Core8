using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Warehouses.Dto
{
    public class WarehouseDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public BranchSharing Sharing { get; set; }
        public string SharingName { get; set; }
        public List<WarehouseBranchDto> WarehouseBranches { get; set; }
    }
}
