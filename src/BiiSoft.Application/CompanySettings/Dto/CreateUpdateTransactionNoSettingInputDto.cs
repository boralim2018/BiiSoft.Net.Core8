using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CompanySettings.Dto
{
    public class CreateUpdateTransactionNoSettingInputDto
    {
        public Guid? Id { get; set; }
        public JournalType JournalType { get; set; }
        public bool CustomTransactionNoEnable { get; set; }
        public string Prefix { get; set; }
        public int Digits { get; set; }
        public int Start { get; set; }
        public bool RequiredReference { get; set; }
    }
}
