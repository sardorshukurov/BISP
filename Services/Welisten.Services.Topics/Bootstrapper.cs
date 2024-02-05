using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Services.Topics;

public static class Bootstrapper
{
    public static IServiceCollection AddTopicService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITopicService, TopicService>();            
    }
}