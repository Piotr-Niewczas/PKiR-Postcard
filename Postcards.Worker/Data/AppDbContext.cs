using Microsoft.EntityFrameworkCore;
using Postcards.Models;

namespace Postcards.Worker.Data;

public class AppDbContext : DbContext
{
    public DbSet<Postcard> Postcards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            @"Host=localhost;Port=5432;Database=postcards;Username=postcardUser;Password=postcard");
    }
}