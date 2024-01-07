using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Common.Helpers;

public static class ValidatorsRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("welisten."));

        assemblies.ToList().ForEach(x => { services.AddValidatorsFromAssembly(x, ServiceLifetime.Singleton); });
    }
}