using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{

    [Table("BiiItemCodeFormulas")]
    public class ItemCodeFormula : ActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public List<ItemType> ItemTypes { get; private set; }
        public ItemCodeFormulaType Type { get; private set; }
        public string Prefix { get; private set; }
        public int Digits { get; private set; }
        public int Start { get; private set; }

        public static ItemCodeFormula Create(int tenantId, long userId, List<ItemType> itemTypes, ItemCodeFormulaType type, string prefix, int digits, int start)
        {
            return new ItemCodeFormula
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                ItemTypes = itemTypes,
                Type = type,
                Prefix = prefix,
                Digits = digits,
                Start = start
            };
        }

        public void Update(long userId, List<ItemType> itemTypes, ItemCodeFormulaType type, string prefix, int digits, int start)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            ItemTypes = itemTypes;
            Type = type;
            Prefix = prefix;
            Digits = digits;
            Start = start;
        }
       
    }
}
