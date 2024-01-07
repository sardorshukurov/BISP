using Microsoft.Extensions.DependencyInjection;
using Welisten.Services.Logger.Logger;

namespace Welisten.Services.Logger;

public static class Bootstrapper
{
    public static IServiceCollection AddAppLogger(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAppLogger, AppLogger>();
    }
}