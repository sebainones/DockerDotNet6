using DockerDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerDotNet;

public class PizzaDbContext: DbContext
{
    public PizzaDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas { get; set; } = null!;    
}