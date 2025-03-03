using System.Text;
using InstructionRAG.Application.Config;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Application.Services;
using InstructionRAG.Infrastructure.Config;
using InstructionRAG.Infrastructure.Database;
using InstructionRAG.Infrastructure.Repositories;
using InstructionRAG.Infrastructure.Strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

var dbConfig = builder.Configuration.GetSection("DbConfig");
builder.Services.Configure<ApplicationDbConfig>(dbConfig);

var jwtConfig = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtConfig);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<IModelService, ModelService>();
builder.Services.AddSingleton<IModelLoadingStrategy, LocalModelLoader>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
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
    // TODO: убрать хардкод url
    app.MapScalarApiReference(c =>
    {
        c.AddServer(new ScalarServer("http://api.localhost"));
    });
}


app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
