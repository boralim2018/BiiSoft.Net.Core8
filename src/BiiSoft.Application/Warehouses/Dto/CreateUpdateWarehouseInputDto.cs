using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Warehouses.Dto
{
    public class CreateUpdateWarehouseInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public BranchSharing Sharing { get; set; }
        public List<WarehouseBranchDto> WarehouseBranches { get; set; }
    }

}
