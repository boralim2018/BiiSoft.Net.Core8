using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BiiSoft.Branches
{
    [Table("BiiTransactionNoSettings")]
    public class TransactionNoSetting : AuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public JournalType JournalType { get; protected set; }
        public bool CustomTransactionNoEnable { get; protected set; }
        public string Prefix { get; protected set; }
        public int Digits { get; protected set; }
        public int Start {  get; protected set; }
        public bool RequiredReference { get; protected set; }


        public static TransactionNoSetting Create(
            int tenantId,
            long? userId,
            JournalType journalType,
            bool customTransactionNoEnable,
            string prefix,
            int digits,
            int start,
            bool requiredReference)
        {
            return new TransactionNoSetting
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                JournalType = journalType,
                CustomTransactionNoEnable = customTransactionNoEnable,
                Prefix = prefix,
                Digits = digits,
                Start = start,
                RequiredReference = requiredReference
            };
        }

        public void Update(
            long? userId,
            JournalType journalType,
            bool customTransactionNoEnable,
            string prefix,
            int digits,
            int start,
            bool requiredReference)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            JournalType = journalType;
            CustomTransactionNoEnable = customTransactionNoEnable;
            Prefix = prefix;
            Digits = digits;
            Start = start;
            RequiredReference = requiredReference;
        }
    }
}
