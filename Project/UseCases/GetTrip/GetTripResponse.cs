using Project.Models;

namespace Project.UseCases.GetTrip;

// Dont need to auth
public record GetTripResponse(
    ICollection<TripSpot> tripSpots // Talvez mudar para itens específicos depois! 
);