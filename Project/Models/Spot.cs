namespace Project.Models;

public class Spot
{
    public int ID { get; set; }
    public required string Title { get; set; }

    public ICollection<TripSpot>? TripSpots { get; set; }
}