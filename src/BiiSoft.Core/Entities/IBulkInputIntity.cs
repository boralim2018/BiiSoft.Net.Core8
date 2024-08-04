using Abp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BiiSoft.Entities
{
    public interface IBulkInputIntity<TEntity>
    {
        long UserId { get; set; }
        List<TEntity> Items { get; set; }
    }

    public interface IMayHaveTenantBulkInputEntity<TEntity> : IBulkInputIntity<TEntity>, IMayHaveTenant
    {

    }

}
