using Microsoft.AspNetCore.Mvc;
using Project.UseCases.CreateTrip;
using Project.UseCases.GetTrip;

namespace Project.Endpoints;

public static class TripEndpoints
{
    public static void ConfigureTripEndpoints(this WebApplication app)
    {
        // GetTrip
        app.MapGet("view-trip/{tripID}", async (
            int tripID,
            [FromServices] GetTripUseCase useCase) =>
        {
            var result = await useCase.Do(tripID); // Mudar depois

            if (!result.IsSuccess)
                return Results.BadRequest();
            return Results.Ok(result.Data);
        }); 
        
        // CreateTrip
        app.MapPost("create-trip", async (
            [FromServices] CreateTripUseCase useCase,
            [FromBody] CreateTripRequest payload) =>
        {
            var result = await useCase.Do(payload); // Mudar Depois

            if (!result.IsSuccess)
                return Results.BadRequest();
            return Results.Ok(result.Data);
        }); 
    }
}