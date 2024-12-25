using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Taxes.Dto
{
    public class TaxListDto : CanModifyDefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string PurchaseAccount { get; set; }
        public string SaleAccount { get; set; }
    }
}
