using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class CommentsContextConfiguration
{
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().ToTable("comments");
        modelBuilder.Entity<Comment>().HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
        modelBuilder.Entity<Comment>().Property(x => x.Text).HasMaxLength(1000).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.Date).IsRequired();
        modelBuilder.Entity<Comment>().Property(x => x.IsAnonymous).IsRequired();
    }
}