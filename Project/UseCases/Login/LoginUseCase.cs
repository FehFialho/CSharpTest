namespace Project.UseCases.Login;

public class LoginUseCase()
{
    public async Task<Result<LoginResponse>> Do(LoginRequest payload) // Mudar para DTO depois
    {
        return Result<LoginResponse>.Success(null);
    }
}