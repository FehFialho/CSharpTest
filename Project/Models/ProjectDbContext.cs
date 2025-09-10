using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Project.Models;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Spot> Spots => Set<Spot>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripSpot> TripSpots => Set<TripSpot>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<TripSpot>()
            .HasOne(ts => ts.Trip)
            .WithMany(t => t.TripSpots)
            .HasForeignKey(ts => ts.TripID)
            .OnDelete(DeleteBehavior.NoAction);

        model.Entity<TripSpot>()
            .HasOne(ts => ts.Spot)
            .WithMany(s => s.TripSpots)
            .HasForeignKey(ts => ts.SpotID)
            .OnDelete(DeleteBehavior.NoAction);

        model.Entity<Trip>()
            .HasOne(t => t.Creator)
            .WithMany(c => c.Trips)
            .HasForeignKey(t => t.CreatorID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public class ProjectDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
{
    public ProjectDbContext CreateDbContext(string[] args)
    {
        // Lê a connection string
        var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION");

        var optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ProjectDbContext(optionsBuilder.Options);
    }
}
