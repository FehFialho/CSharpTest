using Project.Models;
using Project.Services.BeautyDesc;

namespace Project.UseCases.CreateTrip;

public class CreateTripUseCase(
    ProjectDbContext ctx,
    BeautyDesc beautyDesc
)
{
    public async Task<Result<CreateTripResponse>> Do(CreateTripPayload payload) 
    {

        var user = await ctx.Users.FindAsync(payload.userID); 
            Result<CreateTripResponse>.Success(null!);

        if (user is null)
            return Result<CreateTripResponse>.Fail("Usuário não encontrado");

        var trip = new Trip
        {
            Creator = user,
            CreatorID = payload.userID,
            Title = payload.title,
            Description = beautyDesc.RemoveDoubleSpace(payload.description) // Implementado
        };

        ctx.Trips.Add(trip);
        await ctx.SaveChangesAsync();

        return Result<CreateTripResponse>.Success(null!); // Mudar
    }
}