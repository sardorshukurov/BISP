using Welisten.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Identity;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            ;

        return services;
    }
}
