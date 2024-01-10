using Welisten.Context.Seeder;
using Welisten.Services.Logger;
using Welisten.Services.Settings;

namespace Welisten.API;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration)
    {
        service
            .AddMainSettings(configuration)
            .AddLogSettings(configuration)
            .AddSwaggerSettings(configuration)
            .AddAppLogger()
            .AddDbSeeder(configuration)
            ;

        return service;
    }
}