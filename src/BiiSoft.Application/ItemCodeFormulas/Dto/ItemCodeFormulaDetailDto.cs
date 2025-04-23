using Abp.Application.Services.Dto;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class ItemCodeFormulaDetailDto : ActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public bool IsAllItemType { get; set; }
        public List<ItemCodeFormulaItemTypeDto> ItemTypes { get; set; }
        public ItemCodeFormulaType Type { get; set; }
        public string TypeName { get; set; }
        public string Prefix { get; set; }
        public int Digits { get; set; }
        public int Start { get; set; }
    }
}
