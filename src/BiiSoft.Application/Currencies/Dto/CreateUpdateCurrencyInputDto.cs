﻿using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies.Dto
{
    public class CreateUpdateCurrencyInputDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public bool IsDefault { get; set; }
    }

}
