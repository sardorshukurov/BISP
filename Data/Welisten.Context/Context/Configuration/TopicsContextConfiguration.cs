using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class TopicsContextConfiguration
{
    public static void ConfigureTopics(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>().ToTable("Topics");
        modelBuilder.Entity<Topic>().Property(x => x.Type).IsRequired();
        modelBuilder.Entity<Topic>().HasMany(x => x.Posts)
            .WithMany(x => x.Topics)
            .UsingEntity(t => t.ToTable("PostsTopics"));
    }
}