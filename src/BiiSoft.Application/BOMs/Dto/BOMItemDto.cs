using System;

namespace BiiSoft.BOMs.Dto
{
    public class BOMItemDto 
    {
        public Guid? Id { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Qty { get; set; }

    }
}
