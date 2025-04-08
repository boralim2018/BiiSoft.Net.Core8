using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class ItemCodeFormulaItemTypeDto
    {
        public ItemType ItemType { get; set; }
        public string ItemTypeName => ItemType.GetName();
    }

}
