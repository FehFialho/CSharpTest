namespace Project.UseCases.AddSpot;

public record AddSpotRequest(
    int tripID,
    int spotID // Previously registered in the DB
);