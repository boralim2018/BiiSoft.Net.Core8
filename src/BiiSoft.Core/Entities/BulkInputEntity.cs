using Abp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BiiSoft.Entities
{
    public class BulkInputEntity<TEntity> : IBulkInputIntity<TEntity>
    {
        public long UserId { get; set; }
        public List<TEntity> Items { get; set; }
    }

    public class MayHaveTenantBulkInputEntity<TEntity> : BulkInputEntity<TEntity>, IMayHaveTenantBulkInputEntity<TEntity>
    {
        public int? TenantId { get; set; }
    }
}
