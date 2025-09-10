namespace Project.UseCases.GetTrip;

public class GetTripUseCase()
{
    public async Task<Result<GetTripResponse>> Do(GetTripRequest payload) // Mudar para DTO depois
    {
        return Result<GetTripResponse>.Success(null); // Mudar
    }
}