using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Endpoints;
using Project.Models;
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

// Services
builder.Services.AddSingleton<IJWTService, JWTService>();

// UseCases
// builder.Services.AddSingleton<LoginUseCase>();
builder.Services.AddTransient<CreateTripUseCase>();
builder.Services.AddTransient<AddSpotUseCase>();
builder.Services.AddTransient<GetTripUseCase>();

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

app.MapGet("/", () => "Hello World!");

app.Run();