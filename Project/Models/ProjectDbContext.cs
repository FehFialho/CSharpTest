using Microsoft.EntityFrameworkCore;
using Project.Models;

public class ProjectDbContext(DbContextOptions options) : DbContext(options)
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