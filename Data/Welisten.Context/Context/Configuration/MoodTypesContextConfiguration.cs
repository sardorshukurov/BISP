using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class MoodTypesContextConfiguration
{
    public static void ConfigureMoodTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MoodType>().ToTable("moodtypes");
        modelBuilder.Entity<MoodType>().Property(x => x.Name).HasMaxLength(50).IsRequired();
    }
}