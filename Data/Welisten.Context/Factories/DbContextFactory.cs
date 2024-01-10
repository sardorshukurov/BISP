using Microsoft.EntityFrameworkCore;
using Welisten.Context.Context;

namespace Welisten.Context.Factories;

public class DbContextFactory(DbContextOptions<MainDbContext> options)
{
    public MainDbContext Create()
    {
        return new MainDbContext(options);
    }
}