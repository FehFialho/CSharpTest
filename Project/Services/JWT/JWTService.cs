using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Project.Services.JWT;

public class JWTService : IJWTService
{
    public string CreateToken(ProfileToAuth data)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
        var key = new SymmetricSecurityKey(keyBytes);

        var jwt = new JwtSecurityToken(
            // CONFIGURAR CLAIMS!
            claims: [
                //Nunca colocar a senha do usuario, pois não se passa dados criptografados no JWT para não ter vazamento de dados sensveis
                new Claim(ClaimTypes.NameIdentifier, data.ProfileId.ToString()),
                new Claim(ClaimTypes.Name, data.Username),
                //ARRUMAR AQUI
                new Claim(ClaimTypes.Role, "plan")
            ],
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(jwt);
    }
}