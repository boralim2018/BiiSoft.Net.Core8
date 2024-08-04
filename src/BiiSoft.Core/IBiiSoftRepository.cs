using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft
{
    public interface IBiiSoftRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        //IQueryable<TEntity> FromSql(FormattableString sqlQuery);
        //IQueryable<TEntity> FromSqlRaw(string sqlQuery);

        Task BulkInsertAsync(IList<TEntity> list);
        Task BulkInsertAsync(TEntity entity);
        Task BulkUpdateAsync(IList<TEntity> list);
        Task BulkUpdateAsync(TEntity entity);
        Task BulkDeleteAsync(IList<TEntity> list);
        Task BulkDeleteAsync(TEntity list);
        Task BulkInsertOrUpdateAsync(IList<TEntity> list);
        Task BulkInsertOrUpdateAsync(TEntity entity);
        Task BulkInsertOrUpdateOrDeleteAsync(IList<TEntity> list);
        Task BulkInsertOrUpdateOrDeleteAsync(TEntity entity);
        Task BulkReadAsync(IList<TEntity> list);
        Task BulkSaveChangesAsync();

    }

    public interface IBiiSoftRepository<TEntity> : IBiiSoftRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {

    }
}
