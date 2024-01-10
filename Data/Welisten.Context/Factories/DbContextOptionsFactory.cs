using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Welisten.Context.Context;
using Welisten.Context.Settings;

namespace Welisten.Context.Factories;

public static class DbContextOptionsFactory
{
    private const string MigrationProjectPrefix = "Welisten.Context.Migrations.";

    public static DbContextOptions<MainDbContext> Create(
        string connectionString, DbType dbType, bool detailedLogging = false)
    {
        var builder = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connectionString, dbType, detailedLogging).Invoke(builder);

        return builder.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(
        string connectionString, DbType dbType, bool detailedLogging = false)
    {
        return (builder) =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    builder.UseSqlServer(connectionString,
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{MigrationProjectPrefix}{DbType.MSSQL}")
                    );
                    break;

                case DbType.PgSql:
                    builder.UseNpgsql(connectionString,
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{MigrationProjectPrefix}{DbType.PgSql}")
                    );
                    break;

                case DbType.MySql:
                    builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .SchemaBehavior(MySqlSchemaBehavior.Ignore)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{MigrationProjectPrefix}{DbType.MySql}")
                    );
                    break;
            }

            if (detailedLogging)
            {
                builder.EnableSensitiveDataLogging();
            }
            
            builder.UseLazyLoadingProxies(true);
            //builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        };
    }
}