using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Services.Likes;

public static class Bootstrapper
{
    public static IServiceCollection AddLikeService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ILikeService, LikeService>();            
    }
}