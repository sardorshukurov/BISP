using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class LikeContextConfiguration
{
    public static void ConfigureLikes(this ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Like>()
        //     .ToTable("Likes");
        //
        // modelBuilder.Entity<Like>()
        //     .HasKey(l => new { l.UserId, l.PostId });
        //
        // modelBuilder.Entity<Like>()
        //     .HasOne(l => l.Post)
        //     .WithMany(p => p.Likes)
        //     .HasForeignKey(l => l.PostId);
    }
}