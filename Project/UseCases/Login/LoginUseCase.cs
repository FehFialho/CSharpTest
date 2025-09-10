using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services.JWT;

namespace Project.UseCases.Login;

public class LoginUseCase(
    ProjectDbContext ctx,
    JWTService jWTService
)
{
    public async Task<Result<LoginResponse>> Do(LoginRequestt request)
    {
        var user = await ctx.Users.FirstOrDefaultAsync(p => p.Username == request.login);

        if (user is null)
            return Result<LoginResponse>.Fail("Usuário não encontrado!");

        // Comparação de Senhas sem Cripto
        var passwordMatch = request.password == user.Password;

        if (!passwordMatch)
            return Result<LoginResponse>.Fail("Senha Incorreta!");

        // JWT
        var jwt = jWTService.CreateToken(
        new(
            user.ID, user.Username
        ));

        // Se tudo der certo, retorna o JWT
        return Result<LoginResponse>.Success(new LoginResponse(jwt));
    }
}