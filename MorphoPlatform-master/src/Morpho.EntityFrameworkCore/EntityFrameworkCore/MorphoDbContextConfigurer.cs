using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Morpho.EntityFrameworkCore
{
    public static class MorphoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MorphoDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MorphoDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
