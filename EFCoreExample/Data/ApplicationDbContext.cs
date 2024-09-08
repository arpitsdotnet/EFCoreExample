using EFCoreExample.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreExample.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); //Only Required when using IdentityDbContext

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Action", CategoryDisplayOrder = 1 },
            new Category { CategoryId = 2, CategoryName = "SciFi", CategoryDisplayOrder = 2 },
            new Category { CategoryId = 3, CategoryName = "History", CategoryDisplayOrder = 3 }
        );
    }
}
