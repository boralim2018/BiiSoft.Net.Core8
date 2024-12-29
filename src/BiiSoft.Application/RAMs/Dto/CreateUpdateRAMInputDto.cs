﻿using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.RAMs.Dto
{
    public class CreateUpdateRAMInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

}
