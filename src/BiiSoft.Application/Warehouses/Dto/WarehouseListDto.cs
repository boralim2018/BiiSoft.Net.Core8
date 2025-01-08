using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Warehouses.Dto
{
    public class WarehouseListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public BranchSharing Sharing { get; set; }
        public string SharingName { get; set; }
    }
}
