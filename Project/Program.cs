using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Endpoints;
using Project.Models;
using Project.Services.BeautyDesc;
using Project.Services.JWT;
using Project.UseCases.AddSpot;
using Project.UseCases.CreateTrip;
using Project.UseCases.GetTrip;
using Project.UseCases.Login;

var builder = WebApplication.CreateBuilder(args);

// DBContext
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});

// Derrubando o App
builder.Services.AddTransient<IBeautyDesc, BeautyDesc>();
builder.Services.AddTransient<CreateTripUseCase>(); // Só não funciona quando o BeatyDesc é usado nele.

// Combinações testadas em par:
// SING, SING
// SING, TRAN
// SING, SCOP

// TRAN, SING
// TRAN, TRAN
// TRAN, SCOP

// SCOP, SING
// SCOP, TRAN
// SCOP, SCOP

// Serviços e UseCases - Funcionando
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddScoped<GetTripUseCase>();
builder.Services.AddScoped<AddSpotUseCase>(); // Esse e o CreateTrip só precisa colocar o token no header da requisição, já que precisa de JWT

// Derrubando sozinho por causa do LifeTime entre ele e JWT, testado em SCOP, TRAN, SING
// builder.Services.AddTransient<LoginUseCase>(); 

// JWT Vars
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");

if (jwtSecret is null)
    Console.WriteLine("Configure o jwtSecret!");

var keyBytes = Encoding.UTF8.GetBytes(jwtSecret!);
var key = new SymmetricSecurityKey(keyBytes);

// JWT Main
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "ThePixeler", // Lembrar de Trocar o Nome
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key,
        };
    });

builder.Services.AddAuthentication(); // Config JWT
builder.Services.AddAuthorization(); // Config JWT

builder.Services.AddEndpointsApiExplorer(); // Swagger
builder.Services.AddSwaggerGen(); // Swagger

var app = builder.Build();

app.UseSwagger(); // Swagger
app.UseSwaggerUI(); // Swagger

app.UseAuthentication(); // Config JWT
app.UseAuthorization(); // Config JWT

app.ConfigureUserEndpoints();
app.ConfigureSpotEndpoints();
app.ConfigureTripEndpoints();

app.MapGet("/", () => "Vá para /swagger!");

app.Run();