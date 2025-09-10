using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Project.UseCases.AddSpot;

namespace Project.Endpoints;

public static class SpotEndpoints
{
    public static void ConfigureSpotEndpoints(this WebApplication app)
    {
        // AddSpot
        app.MapPut("add-spot", async (
            [FromServices] AddSpotUseCase useCase,
            [FromBody] AddSpotRequest request,
            HttpContext context) =>
        {
            // Pegando ID
            var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null)
                return null;
            var id = int.Parse(claim.Value);

            // Juntando Infos
            var dto = new AddSpotPayload(
                id,
                request.tripID,
                request.spotID
            );      

            var result = await useCase.Do(dto); 

            if (!result.IsSuccess)
                return Results.BadRequest();
            return Results.Ok(result.Data);
        }).RequireAuthorization(); 
    }
}