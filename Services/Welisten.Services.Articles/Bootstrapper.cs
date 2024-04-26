using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Services.Articles;

public static class Bootstrapper
{
    public static IServiceCollection AddArticleServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IArticleService, ArticleService>();            
    }
}