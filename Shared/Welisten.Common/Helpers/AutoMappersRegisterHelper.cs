using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Common.Helpers;

public static class AutoMappersRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("welisten."));

        services.AddAutoMapper(assemblies);
    }
}