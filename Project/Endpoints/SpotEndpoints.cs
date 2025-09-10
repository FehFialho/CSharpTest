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
            [FromBody] AddSpotRequest request) =>
        {
            var result = await useCase.Do(request); // Mudar Depois

            if (!result.IsSuccess)
                return Results.BadRequest();
            return Results.Ok(result.Data);
        });  // .RequireAuthorization(); depois

    }
}