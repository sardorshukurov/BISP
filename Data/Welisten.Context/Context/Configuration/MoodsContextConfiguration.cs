using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class MoodsContextConfiguration
{
    public static void ConfigureMoods(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mood>().ToTable("moods");
        modelBuilder.Entity<Mood>().Property(x => x.UserId);
        modelBuilder.Entity<Mood>().HasOne(x => x.User).WithMany(x => x.Moods).HasForeignKey(x => x.UserId);
        modelBuilder.Entity<Mood>().Property(x => x.MoodTypeId);
        modelBuilder.Entity<Mood>().HasMany(x => x.MoodTypes).WithOne().HasForeignKey(x => x.Id);
        modelBuilder.Entity<Mood>().Property(x => x.Date).IsRequired();
    }
}