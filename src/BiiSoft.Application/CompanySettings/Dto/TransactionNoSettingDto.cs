using BiiSoft.Enums;
using System;

namespace BiiSoft.CompanySettings.Dto
{
    public class TransactionNoSettingDto
    {
        public Guid? Id { get; set; }
        public JournalType JournalType { get; set; }
        public string JournalTypeName { get; set; }
        public bool CustomTransactionNoEnable { get; set; }
        public string Prefix { get; set; }
        public int Digits { get; set; }
        public int Start { get; set; }
        public bool RequiredReference { get; set; }
    }
}
