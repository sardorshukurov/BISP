using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Welisten.Context.Context;
using Welisten.Context.Entities;
using Welisten.Context.Seeder.Demo;
using Welisten.Context.Settings;

namespace Welisten.Context.Seeder;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static MainDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
            {
                await AddDemoData(serviceProvider);
            })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);
        
        if (!await context.Topics.AnyAsync())
        {
            await context.Topics.AddRangeAsync(DemoHelper.Topics);
            await context.SaveChangesAsync();
        }

        if (!await context.EventTypes.AnyAsync())
        {
            await context.EventTypes.AddRangeAsync(DemoHelper.Events);
            await context.SaveChangesAsync();
        }

        if (!await context.Moods.AnyAsync())
        {
            await context.Moods.AddRangeAsync(DemoHelper.Moods);
            await context.SaveChangesAsync();
        }
        
        await AddAdmin(serviceProvider);
    }

    private static async Task AddAdmin(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var adminRoleExists = await roleManager.RoleExistsAsync("Administrator");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Administrator"));
            }

            var user = new User
            {
                Name = "admin",
                FirstName = "admin",
                UserName = "admin@example.com",
                Email = "admin@example.com"
            };

            var createUserResult = await userManager.CreateAsync(user, "AdminPassword123!");
            if (createUserResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }

}