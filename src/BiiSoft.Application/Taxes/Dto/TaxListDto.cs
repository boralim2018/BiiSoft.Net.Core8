using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Taxes.Dto
{
    public class TaxListDto : CanModifyDefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public decimal Rate { get; set; }
        public string PurchaseAccountName { get; set; }
        public string SaleAccountName { get; set; }
    }
}
