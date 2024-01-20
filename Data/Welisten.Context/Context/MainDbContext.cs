using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Welisten.Context.Context.Configuration;
using Welisten.Context.Entities;

namespace Welisten.Context.Context;

public class MainDbContext
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostCount> PostCounts { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<MoodType> MoodTypes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ConfigureComments();
        builder.ConfigureMoods();
        builder.ConfigureMoodTypes();
        builder.ConfigurePosts();
        builder.ConfigurePostCounts();
        builder.ConfigureReactions();
        builder.ConfigureTopics();
        builder.ConfigureUsers();
    }
}