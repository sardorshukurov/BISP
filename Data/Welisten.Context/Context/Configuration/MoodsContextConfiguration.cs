using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class MoodRecordContextConfiguration
{
    public static void ConfigureMoodRecords(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MoodRecord>().ToTable("MoodRecords");
        modelBuilder.Entity<MoodRecord>().Property(x => x.Date).IsRequired();

        modelBuilder.Entity<MoodRecord>()
            .HasOne(md => md.Mood)
            .WithMany()
            .IsRequired();

        modelBuilder.Entity<MoodRecord>()
            .HasOne(mr => mr.User)
            .WithMany(u => u.MoodRecords)
            .HasForeignKey(mr => mr.UserId)
            .IsRequired();

        modelBuilder.Entity<MoodRecord>()
            .HasOne(x => x.Event);
    }
}

public static class MoodContextConfiguration
{
    public static void ConfigureMoods(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mood>().ToTable("Moods");
        modelBuilder.Entity<Mood>().Property(x => x.Name).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Mood>().Property(x => x.ImageLink).HasMaxLength(2048).IsRequired();
    }
}

public static class EventTypeContextConfiguration
{
    public static void ConfigureEventTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>().ToTable("EventTypes");
        modelBuilder.Entity<EventType>().Property(x => x.Name).HasMaxLength(50).IsRequired();
    }
}