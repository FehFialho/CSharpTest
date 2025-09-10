namespace Project.UseCases.CreateTrip;

public record CreateTripPayload(
    int userID,
    string title,
    string description
);