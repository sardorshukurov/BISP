using Microsoft.EntityFrameworkCore;

namespace Welisten.Context.Context;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    // public DbSet<TestEntity> TestEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.ConfigureTestEntities();
    }
}

// public class TestEntity
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
// }
//
// public static class TestEntityContextConfiguration
// {
//     public static void ConfigureTestEntities(this ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<TestEntity>().ToTable("categories");
//         modelBuilder.Entity<TestEntity>().Property(x => x.Name).IsRequired();
//         modelBuilder.Entity<TestEntity>().Property(x => x.Name).HasMaxLength(100);
//     }
// }