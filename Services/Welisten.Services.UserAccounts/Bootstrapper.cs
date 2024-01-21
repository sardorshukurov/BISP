using Microsoft.Extensions.DependencyInjection;

namespace Welisten.Services.UserAccounts;

public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountService, UserAccountService>();

        return services;
    }
}