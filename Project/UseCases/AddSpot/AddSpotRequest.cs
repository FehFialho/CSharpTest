namespace Project.UseCases.AddSpot;

public record AddSpotRequest(
    int tripID,
    string spotID // Previously registered in the DB
);