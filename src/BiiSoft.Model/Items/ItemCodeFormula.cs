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
    public class ItemCodeFormula : ActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        public ICollection<ItemCodeFormulaItemType> ItemTypes { get; private set; }
        public ItemCodeFormulaType Type { get; private set; }
        public string Prefix { get; private set; }
        public int Digits { get; private set; }
        public int Start { get; private set; }

        public static ItemCodeFormula Create(int tenantId, long userId, ItemCodeFormulaType type, string prefix, int digits, int start)
        {
            return new ItemCodeFormula
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                ItemTypes = new List<ItemCodeFormulaItemType>(),
                Type = type,
                Prefix = prefix,
                Digits = digits,
                Start = start,
                IsActive = true
            };
        }

        public void Update(long userId, ItemCodeFormulaType type, string prefix, int digits, int start)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Type = type;
            Prefix = prefix;
            Digits = digits;
            Start = start;
        }
       
    }
}
