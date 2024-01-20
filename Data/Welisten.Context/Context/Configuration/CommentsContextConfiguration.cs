using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class CommentsContextConfiguration
{
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().ToTable("comments");
        modelBuilder.Entity<Comment>().Property(x => x.PostId).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(1000).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.Date).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.IsAnonymous).IsRequired();
        modelBuilder.Entity<Comment>().HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId);
    }
}