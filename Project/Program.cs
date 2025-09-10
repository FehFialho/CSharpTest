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

// Derrubando 
// builder.Services.AddScoped<IBeautyDesc, BeautyDesc>();
builder.Services.AddTransient<CreateTripUseCase>(); // Só não funciona quando com o Serviço BeautyDesc
// combinações testadas:
// SING, SING
// SING, TRAN
// SING, SCOP

// TRAN, SING
// TRAN, TRAN
// TRAN, SCOP

// SCOP, SING
// SCOP, TRAN
// SCOP, SCOP

// builder.Services.AddTransient<LoginUseCase>();
// Derrubando sozinho, testado em SCOP, TRAN, SING

// Serviços e UseCases - Funcionando
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddScoped<AddSpotUseCase>();
builder.Services.AddScoped<GetTripUseCase>();

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

app.Run();