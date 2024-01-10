using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Welisten.Common.Settings;
using Welisten.Context.Context;
using Welisten.Context.Factories;
using Welisten.Context.Settings;

namespace Welisten.Context;

public static class Bootstrapper
{
    /// <summary>
    /// Register db context
    /// </summary>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = CommonSettings.Load<DbSettings>("Database", configuration);
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString, settings.Type, true);

        services.AddDbContextFactory<MainDbContext>(dbInitOptionsDelegate);

        return services;
    }
}