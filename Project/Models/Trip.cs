namespace Project.Models;

public class Trip
{
    public int ID { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}