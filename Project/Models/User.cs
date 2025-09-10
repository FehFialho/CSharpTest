namespace Project.Models;

public class User
{
    public Guid ID { get; set; }
    public required string Username { get; set; }
    public required string CompleteName { get; set; }
    public required string Password { get; set; }

    ICollection<Trip>? Trips { get; set; }
}