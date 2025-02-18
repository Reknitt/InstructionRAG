using InstructionRAG.Application.Interfaces;
using InstructionRAG.Application.Services;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using InstructionRAG.Infrastructure.Database;
using InstructionRAG.Infrastructure.Models;
using InstructionRAG.Infrastructure.Repositories;
using InstructionRAG.Infrastructure.Strategies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.AI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Configuration.AddJsonFile("appsettings.json", true, true);

var modelConfig = builder.Configuration.GetSection("ModelConfig");
builder.Services.Configure<ModelConfig>(modelConfig);

var sqliteDatabaseConfig = builder.Configuration.GetSection("SqliteDatabaseConfig");
builder.Services.Configure<SqliteDatabaseConfig>(sqliteDatabaseConfig);

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
builder.Services.AddDbContextFactory<SqliteDbContext>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SqliteDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
