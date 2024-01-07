using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Welisten.Common.Settings;
using Welisten.Services.Settings.AppSettings;

namespace Welisten.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddMainSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = CommonSettings.Load<MainSettings>("Main", configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = CommonSettings.Load<SwaggerSettings>("Swagger", configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddLogSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = CommonSettings.Load<LogSettings>("Log", configuration);
        services.AddSingleton(settings);

        return services;
    }
}