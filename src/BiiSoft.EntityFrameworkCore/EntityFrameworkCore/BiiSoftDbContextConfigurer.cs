using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BiiSoft.EntityFrameworkCore
{
    public static class BiiSoftDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BiiSoftDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BiiSoftDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
