namespace Project.Models;

public class Trip
{
    public int ID { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required int CreatorID { get; set; }

    public required User Creator { get; set; }
    public ICollection<TripSpot>? TripSpots { get; set; }
}