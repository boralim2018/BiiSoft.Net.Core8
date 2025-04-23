using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class ItemCodeFormulaListDto : ActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public bool IsAllItemType { get; set; }
        public List<string> ItemTypes { get; set; }
        public string Type { get; set; }
        public string Prefix { get; set; }
        public int Digits { get; set; }
        public int Start { get; set; }

    }
}
