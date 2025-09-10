using System.Security.Claims;
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
            [FromBody] CreateTripRequest request,
            HttpContext context) =>
        {
            // Pegando ID
            var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null)
                return null;
            var id = int.Parse(claim.Value);

            // Juntando Infos
            var dto = new CreateTripPayload(
                id,
                request.title,
                request.description
            );      

            var result = await useCase.Do(dto);

            if (!result.IsSuccess)
                return Results.BadRequest();
            return Results.Ok(result.Data);
        }); 
    }
}