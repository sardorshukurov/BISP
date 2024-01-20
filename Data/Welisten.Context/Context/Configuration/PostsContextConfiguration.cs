using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class PostsContextConfiguration
{
    public static void ConfigurePosts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().ToTable("posts");
        modelBuilder.Entity<Post>().Property(x => x.Title).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Post>().Property(x => x.Text).HasMaxLength(3000).IsRequired();
        modelBuilder.Entity<Post>().Property(x => x.Date).IsRequired();
        modelBuilder.Entity<Post>().Property(x => x.IsAnonymous).IsRequired();
        modelBuilder.Entity<Post>().HasMany(x => x.Reactions)
            .WithMany(x => x.Posts)
            .UsingEntity(t => t.ToTable("posts_reactions"));
        modelBuilder.Entity<Post>().HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId);
    }
}