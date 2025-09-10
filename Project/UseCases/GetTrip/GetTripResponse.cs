using Project.Models;

namespace Project.UseCases.GetTrip;

// Dont need to auth
public record GetTripResponse(
    string creatorName,
    string title,
    string description,
    ICollection<string> Spots
    // ICollection<TripSpot> tripSpots // Talvez mudar para itens específicos depois! 
);