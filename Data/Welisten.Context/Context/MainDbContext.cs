using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Welisten.Context.Context.Configuration;
using Welisten.Context.Entities;
using Welisten.Context.Entities.Articles;

namespace Welisten.Context.Context;

public class MainDbContext
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostCount> PostCounts { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<MoodRecord> MoodRecords { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleCategory> ArticleCategories { get; set; }
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ConfigurePosts();
        //builder.ConfigurePostCounts(); 
        builder.ConfigureComments(); 
        builder.ConfigureTopics(); 
        builder.ConfigureUsers();
        builder.ConfigureMoodRecords();
        builder.ConfigureMoods();
        builder.ConfigureEventTypes();
    }
}