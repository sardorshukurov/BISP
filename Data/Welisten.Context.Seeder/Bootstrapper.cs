using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Context.Seeder;

public static class Bootstrapper
{
    /// <summary>
    /// Register db seeder
    /// </summary>
    public static IServiceCollection AddDbSeeder(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}