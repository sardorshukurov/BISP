using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Services.Moods;

public static class Bootstrapper
{
    public static IServiceCollection AddMoodService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMoodService, MoodService>();
    }
}