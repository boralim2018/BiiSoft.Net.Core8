using System;

namespace BiiSoft.Warehouses.Dto
{
    public class WarehouseBranchDto 
    {
        public Guid? Id { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

    }
}
