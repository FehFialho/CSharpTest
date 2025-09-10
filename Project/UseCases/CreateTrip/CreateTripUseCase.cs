using Project.Models;

namespace Project.UseCases.CreateTrip;

public class CreateTripUseCase(
    ProjectDbContext ctx
    // Criar Extract JWT para inserir criador!
)
{
    public async Task<Result<CreateTripResponse>> Do(CreateTripRequest request) // Mudar para DTO depois
    {

        var user = await ctx.Users.FindAsync(1); // Temporário até implementar JWT Extractor!
            Result<CreateTripResponse>.Success(null!);
        if (user is null)
            return Result<CreateTripResponse>.Fail("Usuário não encontrado");

        var trip = new Trip
        {
            // Creator ID e Creator com JWT Depois!
            Creator = user,
            CreatorID = 1,
            Title = request.title,
            Description = request.description
        };

        ctx.Trips.Add(trip);
        await ctx.SaveChangesAsync();

        return Result<CreateTripResponse>.Success(null!); // Mudar
    }
}