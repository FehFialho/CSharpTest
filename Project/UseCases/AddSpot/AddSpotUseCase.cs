using Project.Models;

namespace Project.UseCases.AddSpot;

public class AddSpotUseCase(
    ProjectDbContext ctx
    // Criar Extract JWT para inserir criador!
)
{
    public async Task<Result<AddSpotResponse>> Do(AddSpotRequest request) // Mudar para DTO depois
    {

        var spot = await ctx.Spots.FindAsync(request.spotID);

        if (spot is null)
            return Result<AddSpotResponse>.Fail("Ponto não encontrado!");    

        var trip = await ctx.Trips.FindAsync(request.tripID);
        
        if (trip is null)
            return Result<AddSpotResponse>.Fail("Viagem não encontrada!");    

        var tripSpot = new TripSpot
        {
            TripID = request.tripID,
            SpotID = request.spotID,
            Trip = trip,
            Spot = spot
        };

        ctx.TripSpots.Add(tripSpot);
        await ctx.SaveChangesAsync();

        return Result<AddSpotResponse>.Success(new());
    }
}