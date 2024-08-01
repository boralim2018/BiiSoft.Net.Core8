using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{ 

    public class UserEntity<TPrimaryKey> : Entity<TPrimaryKey>, IUserEntity<TPrimaryKey>
    {
        public long? UserId { get; set; }
    }

    public class ImportExcelEntity<TPrimaryKey> : UserEntity<TPrimaryKey>, IImportExcelEntity<TPrimaryKey>
    {
        public int? TenantId { get; set; }
        public string Token { get; set; }
    }
}
