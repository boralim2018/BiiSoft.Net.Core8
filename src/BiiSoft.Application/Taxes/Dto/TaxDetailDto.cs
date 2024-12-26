using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Taxes.Dto
{
    public class TaxDetailDto : CanModifyDefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public decimal Rate { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public string PurchaseAccountName { get; set; }
        public Guid? SaleAccountId { get; set; }
        public string SaleAccountName { get; set; }
    }
}
