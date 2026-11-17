using LinqPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqPractice.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
    {
        var connectionString =
            Environment.GetEnvironmentVariable(
                "ConnectionStrings__Default"
            );

        options.UseNpgsql(connectionString);
    }
}