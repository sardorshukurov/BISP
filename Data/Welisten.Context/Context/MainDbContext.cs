using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Welisten.Context.Context.Configuration;
using Welisten.Context.Entities.User;

namespace Welisten.Context.Context;

public class MainDbContext
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ConfigureUsers();
    }
}