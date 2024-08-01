using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Countries.Dto
{
    public class CreateUpdateCountryInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string ISO { get; set; }
        public string ISO2 { get; set; }
        public string PhonePrefix { get; set; }
        public long? CurrencyId { get; set; }
    }

}
