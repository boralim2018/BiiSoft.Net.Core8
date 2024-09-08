using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Dtos
{
    public interface ICreatorAuditedDto
    {
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime? CreationTime { get; set; }
    }

    public interface IModifierAuditedDto
    {
        public long? LastModifierUserId { get; set; }
        public string LastModifierUserName { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

}
