using Welisten.Context.Seeder;
using Welisten.Services.Comments;
using Welisten.Services.Logger;
using Welisten.Services.Posts;
using Welisten.Services.Settings;
using Welisten.Services.UserAccounts;

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
            .AddPostService()
            .AddCommentService()
            .AddUserAccountService();
            ;

        return service;
    }
}