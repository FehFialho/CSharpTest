namespace Project.Models;

public class User
{
    public int ID { get; set; } // Usando ID pra facilitar inserção manual no DB
    public required string Username { get; set; }
    public required string CompleteName { get; set; }
    public required string Password { get; set; }

    public ICollection<Trip>? Trips { get; set; }
}