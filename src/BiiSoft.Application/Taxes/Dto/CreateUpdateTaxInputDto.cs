using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Taxes.Dto
{
    public class CreateUpdateTaxInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public decimal Rate { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
    }

}
