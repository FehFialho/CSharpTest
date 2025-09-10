namespace Project.UseCases.Login;

public record LoginRequest(
    string login,
    string password
);