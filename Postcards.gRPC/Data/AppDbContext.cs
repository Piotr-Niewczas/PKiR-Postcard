using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Postcards.Models;

namespace Postcards.gRPC.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Postcard> Postcards { get; set; }
    
}