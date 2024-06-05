using Microsoft.EntityFrameworkCore;
using Postcards.Models;

namespace Postcards.gRPC.Data;

public class AppDbContext(IConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public DbSet<Postcard> Postcards { get; set; }
    
}