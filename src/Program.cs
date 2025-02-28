using System.Text;
using InstructionRAG.Application.Config;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Application.Services;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using InstructionRAG.Infrastructure.Database;
using InstructionRAG.Infrastructure.Models;
using InstructionRAG.Infrastructure.Repositories;
using InstructionRAG.Infrastructure.Strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Configuration.AddJsonFile("./src/appsettings.json", true, true);

var modelConfig = builder.Configuration.GetSection("ModelConfig");
builder.Services.Configure<ModelConfig>(modelConfig);

var postgresDatabaseConfig= builder.Configuration.GetSection("PostgresDbConfig");
builder.Services.Configure<PostgresDbConfig>(postgresDatabaseConfig);

var jwtConfig = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtConfig);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// TODO: почитать про другие варианты инжекта зависимостей и заменить
builder.Services.AddSingleton<IModelService, ModelService>();
builder.Services.AddSingleton<IModelLoadingStrategy, LocalModelLoader>();
builder.Services.AddSingleton<IChatRepository, ChatRepository>();
builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddDbContext<ApplicationDbContext>();


builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    string secret = jwtConfig.GetValue<string>("Secret");
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = false;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
