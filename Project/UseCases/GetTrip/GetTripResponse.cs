using Project.Models;

namespace Project.UseCases.GetTrip;

// Dont need to auth
public record GetTripResponse(
    Trip trip
    // string creatorName,
    // string title,
    // string description,
    // ICollection<string> Spots
);