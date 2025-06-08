using APDB_Kolokwium_template.Models;
using Microsoft.EntityFrameworkCore;

namespace APDB_Kolokwium_template.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Other> Others { get; set; }
    public DbSet<Example> Examples { get; set; }
    
    
    public AppDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var others = new List<Other>
        {
            new()
            {
                Id = 1,
                Text = "Some text"
            },
            new()
            {
                Id = 2,
                Text = "Some other text"
            }
        };

        var examples = new List<Example>
        {
            new()
            {
                Id = 1,
                Text = "Some text",
                OtherId = 1
            },
            new()
            {
                Id = 2,
                Text = "Some other text",
                OtherId = 2
            },
            new()
            {
                Id = 3,
                Text = "I'm a different example",
                OtherId = 2
            }
        };
        
        modelBuilder.Entity<Other>().HasData(others);
        modelBuilder.Entity<Example>().HasData(examples);
    }
    
}