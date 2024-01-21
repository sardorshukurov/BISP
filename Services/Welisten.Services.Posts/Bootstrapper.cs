using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Services.Posts;

public static class Bootstrapper
{
    public static IServiceCollection AddPostService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPostService, PostService>();            
    }
}