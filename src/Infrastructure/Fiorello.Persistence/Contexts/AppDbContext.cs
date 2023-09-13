using Fiorello.Domain.Entities;
using Fiorello.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Persistence.Contexts;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    { 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Category> Categories { get; set; } = null!;
}
