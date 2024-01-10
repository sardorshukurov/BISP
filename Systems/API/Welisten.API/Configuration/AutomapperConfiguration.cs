using Welisten.Common.Helpers;

namespace Welisten.API.Configuration;

/// <summary>
/// AutoMapper configuration
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Add AutoMappers
    /// </summary>
    /// <param name="services">Services collection</param>
    public static IServiceCollection AddAppAutoMappers(this IServiceCollection services)
    {
        AutoMappersRegisterHelper.Register(services);

        return services;
    }
}