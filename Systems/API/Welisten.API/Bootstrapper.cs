using Welisten.Context.Seeder;
using Welisten.Services.Comments;
using Welisten.Services.Likes;
using Welisten.Services.Logger;
using Welisten.Services.Moods;
using Welisten.Services.Posts;
using Welisten.Services.Settings;
using Welisten.Services.Topics;
using Welisten.Services.UserAccounts;

namespace Welisten.API;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration)
    {
        service
            .AddMainSettings(configuration)
            .AddLogSettings(configuration)
            .AddSwaggerSettings(configuration)
            .AddAppLogger()
            .AddDbSeeder(configuration)
            .AddPostService()
            .AddCommentService()
            .AddUserAccountService()
            .AddLikeService()
            .AddTopicService()
            .AddMoodService()
            ;

        return service;
    }
}