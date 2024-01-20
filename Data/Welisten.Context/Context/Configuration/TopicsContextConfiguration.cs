using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class TopicsContextConfiguration
{
    public static void ConfigureTopics(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>().ToTable("topics");
        modelBuilder.Entity<Topic>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Topic>().HasMany(x => x.Users).WithMany(x => x.Topics);
    }
}