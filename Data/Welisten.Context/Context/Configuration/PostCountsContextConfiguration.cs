using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class PostCountsContextConfiguration
{
    public static void ConfigurePostCounts(this ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<PostCount>()
        //     .HasKey(pc => pc.PostId);
        //
        // modelBuilder.Entity<PostCount>()
        //     .Property(pc => pc.CommentCount)
        //     .IsRequired();
        //
        // modelBuilder.Entity<PostCount>()
        //     .Property(pc => pc.LikeCount)
        //     .IsRequired();
    }
}