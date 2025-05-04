using System;
using Abp.Domain.Entities;

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
    public class UpdateFileEntity<TPrimaryKey> : UserEntity<TPrimaryKey>, IUpdateFileEntity<TPrimaryKey>
    {
        public int? TenantId { get; set; }
        public Guid FileId { get; set; }
    }
}
