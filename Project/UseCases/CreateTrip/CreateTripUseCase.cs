namespace Project.UseCases.CreateTrip;

public class CreateTripUseCase()
{
    public async Task<Result<CreateTripResponse>> Do(CreateTripRequest payload) // Mudar para DTO depois
    {
        return Result<CreateTripResponse>.Success(null); // Mudar
    }
}