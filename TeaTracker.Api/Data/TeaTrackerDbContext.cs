using Microsoft.EntityFrameworkCore;
using TeaTracker.Api.Models;

namespace TeaTracker.Api.Data;

public class TeaTrackerDbContext(DbContextOptions<TeaTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Tea> Teas { get; set; }
    public DbSet<Tasting> Tastings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tea>()
            .Property(t => t.TeaType)
            .HasConversion<string>();
    }
}
 