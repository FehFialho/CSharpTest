using Project.Models;

namespace Project.UseCases.AddSpot;

public class AddSpotUseCase(
    ProjectDbContext ctx
    // Criar Extract JWT para inserir criador!
)
{
    public async Task<Result<AddSpotResponse>> Do(AddSpotPayload payload) // Mudar para DTO depois
    {

        var spot = await ctx.Spots.FindAsync(payload.spotID);

        if (spot is null)
            return Result<AddSpotResponse>.Fail("Ponto não encontrado!");    

        var trip = await ctx.Trips.FindAsync(payload.tripID);
        
        if (trip is null)
            return Result<AddSpotResponse>.Fail("Viagem não encontrada!");    

        var tripSpot = new TripSpot
        {
            TripID = payload.tripID,
            SpotID = payload.spotID,
            Trip = trip,
            Spot = spot
        };

        ctx.TripSpots.Add(tripSpot);
        await ctx.SaveChangesAsync();

        return Result<AddSpotResponse>.Success(new());
    }
}