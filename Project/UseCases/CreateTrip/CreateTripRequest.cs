using System.ComponentModel.DataAnnotations;

namespace Project.UseCases.CreateTrip;

public record CreateTripRequest
{
    [MaxLength(20)]
    public string title { get; set; }

    [MinLength(40)]
    [MaxLength(200)]
    public string description { get; set; }
}