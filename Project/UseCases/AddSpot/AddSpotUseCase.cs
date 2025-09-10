using Project.Models;

namespace Project.UseCases.AddSpot;

public class AddSpotUseCase(
    ProjectDbContext ctx
    // Criar Extract JWT
)
{
    public async Task<Result<AddSpotResponse>> Do(AddSpotRequest payload) // Mudar para DTO depois
    {
        return Result<AddSpotResponse>.Success(new());
    }
}