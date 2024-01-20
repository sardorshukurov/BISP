using Microsoft.EntityFrameworkCore;
using Welisten.Context.Entities;

namespace Welisten.Context.Context.Configuration;

public static class ReactionsContextConfiguration
{
    public static void ConfigureReactions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reaction>().ToTable("reactions");
        modelBuilder.Entity<Reaction>().Property(x => x.Type).IsRequired();
    }
}