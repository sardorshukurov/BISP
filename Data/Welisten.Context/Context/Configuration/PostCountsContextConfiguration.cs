using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class PostCountsContextConfiguration
{
    public static void ConfigurePostCounts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostCount>().ToTable("PostCounts");
        modelBuilder.Entity<PostCount>().HasOne(x => x.Post).WithOne(x => x.PostCount).HasPrincipalKey<PostCount>(x => x.Id);
        modelBuilder.Entity<PostCount>().Property(x => x.CommentCount).IsRequired();
        modelBuilder.Entity<PostCount>().Property(x => x.LikeCount).IsRequired();
    }
}