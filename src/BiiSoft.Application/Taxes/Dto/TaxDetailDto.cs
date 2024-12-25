using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Taxes.Dto
{
    public class TaxDetailDto : CanModifyDefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public string PurchaseAccount { get; set; }
        public Guid? SaleAccountId { get; set; }
        public string SaleAccount { get; set; }
    }
}
