using Microsoft.EntityFrameworkCore;
using Postcards.Models;

namespace Postcards.Worker.Data;

public class AppDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Postcard> Postcards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }
}