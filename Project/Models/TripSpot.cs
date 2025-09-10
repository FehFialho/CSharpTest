namespace Project.Models;

public class TripSpot
{
    public int ID { get; set; }
    public required int TripID { get; set; }
    public required int SpotID { get; set; }

    public required Trip Trip { get; set; }
    public required Spot Spot { get; set; }
}