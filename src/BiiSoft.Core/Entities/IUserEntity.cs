using System;
using Abp.Domain.Entities;

namespace BiiSoft.Entities
{
   
    public interface IUserEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        long? UserId { get; set; }
    }

    public interface IImportExcelEntity<TPrimaryKey> : IUserEntity<TPrimaryKey>, IMayHaveTenant
    {
        string Token { get; set; }
    }
    public interface IUpdateFileEntity<TPrimaryKey> : IUserEntity<TPrimaryKey>, IMayHaveTenant
    {
        Guid FileId { get; set; }
    }
}
