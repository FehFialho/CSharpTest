using Microsoft.AspNetCore.Mvc;
using Project.UseCases.Login;

namespace Project.Endpoints;

public static class UserEndpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    { 
        // Auth
        app.MapPost("auth", async (
            [FromBody] LoginRequestt request,
            [FromServices]LoginUseCase useCase) =>
        {   
            var result = await useCase.Do(request);
            if (!result.IsSuccess)
                return Results.BadRequest();
            return Results.Ok(result.Data);
        });
    }
}