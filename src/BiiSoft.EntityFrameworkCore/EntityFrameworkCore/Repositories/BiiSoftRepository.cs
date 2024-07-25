using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using BiiSoft.Extensions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.EntityFrameworkCore.Repositories
{
    public class BiiSoftRepository<TEntity, TPrimaryKey> : BiiSoftRepositoryBase<TEntity, TPrimaryKey>, IBiiSoftRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

      
        public BiiSoftRepository(IDbContextProvider<BiiSoftDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public IQueryable<TEntity> FromSql(FormattableString sqlQuery)
        {
            return GetContext().Set<TEntity>().FromSql(sqlQuery);
        }

        public IQueryable<TEntity> FromSqlRaw(string sqlQuery)
        {
            return GetContext().Set<TEntity>().FromSqlRaw(sqlQuery);
        }

        public async Task BulkInsertAsync(IList<TEntity> list)
        {
            var context = await this.GetContextAsync(); 
            await context.BulkInsertAsync(list);
        }

        public async Task BulkUpdateAsync(IList<TEntity> list)
        {
            var context = await this.GetContextAsync();
            await context.BulkUpdateAsync(list);
        }

        public async Task BulkDeleteAsync(IList<TEntity> list)
        {
            var context = await this.GetContextAsync();
            await context.BulkDeleteAsync(list);
        }

        public async Task BulkInsertOrUpdateAsync(IList<TEntity> list)
        {
            var context = await this.GetContextAsync();
            await context.BulkInsertOrUpdateAsync(list);            
        }

        public async Task BulkInsertOrUpdateOrDeleteAsync(IList<TEntity> list)
        {
            var context = await this.GetContextAsync();
            await context.BulkInsertOrUpdateOrDeleteAsync(list);
        }

        public async Task BulkReadAsync(IList<TEntity> list)
        {
            var context = await this.GetContextAsync();
            await context.BulkReadAsync(list);
        }

        public async Task BulkSaveChangesAsync()
        {
            var context = await this.GetContextAsync();
            await context.BulkSaveChangesAsync();
        }

    }


    public class BiiSoftRepository<TEntity> : BiiSoftRepository<TEntity, int>, IBiiSoftRepository<TEntity>
       where TEntity : class, IEntity<int>
    {
        public BiiSoftRepository(IDbContextProvider<BiiSoftDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)!!!
    }
}
