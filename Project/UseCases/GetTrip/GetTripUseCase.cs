using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.UseCases.GetTrip;

public class GetTripUseCase(
    ProjectDbContext ctx
)
{
    public async Task<Result<GetTripResponse>> Do(int tripID) 
    {
        var trip = await ctx.Trips.FindAsync(tripID);

        if (trip is null)
            return Result<GetTripResponse>.Fail("Passeio não encontrado!");

        var spots = ctx.TripSpots
            .Where(tp => tp.TripID == tripID)
            .Select(tp => tp.Spot.Title)
            .ToListAsync;
            
        // var response = new GetTripResponse
        // {
        //     creatorName = trip.Creator.CompleteName,
        //     title = trip.Title,
        //     description = trip.Description,
        //     Spots = spots 
        // };

        return Result<GetTripResponse>.Success(new(trip)); // Mudar
    }
}