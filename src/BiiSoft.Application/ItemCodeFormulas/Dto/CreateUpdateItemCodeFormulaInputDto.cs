using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class CreateUpdateItemCodeFormulaInputDto
    {
        public Guid? Id { get; set; }
        public bool IsAllItemType { get; set; }
        public List<ItemCodeFormulaItemTypeDto> ItemTypes { get; set; }
        public ItemCodeFormulaType Type { get; set; }
        public string Prefix { get; set; }
        public int Digits { get; set; }
        public int Start { get; set; }
    }

}
