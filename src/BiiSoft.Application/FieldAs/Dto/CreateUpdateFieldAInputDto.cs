﻿using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.FieldAs.Dto
{
    public class CreateUpdateFieldAInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

}
