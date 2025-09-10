namespace Project.UseCases.CreateTrip;

public record CreateTripRequest(
    string title,
    string description
);