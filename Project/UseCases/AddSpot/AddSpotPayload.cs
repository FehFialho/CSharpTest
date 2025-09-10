namespace Project.UseCases.AddSpot;

public record AddSpotPayload(
    int userID,
    int tripID,
    int spotID // Previously registered in the DB
);