using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InstructionRAG.Application.Config;
using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InstructionRAG.Application.Services;

public class AuthService(
    IUserService userService, 
    ILogger<AuthService> logger,
    IOptions<JwtConfig> jwtConfig
    ) : IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly ILogger _logger = logger;
    private readonly JwtConfig _jwtConfig = jwtConfig.Value;
    
    public async Task<string> Login(LoginUserRequest request)
    {
        User? user = await _userService.GetUserByEmailAsync(request.Email);
        
        if (user == null)
            throw new Exception($"Invalid credentials, user with provided email: {request.Email} does not exist.");
        
        _logger.LogInformation("User with email: {Email} was found.", request.Email);

        var result = VerifyPassword(user.PasswordHash, request.Password);
        
        _logger.LogWarning("Result of password verification is {Result}", result); 
        
        if (!result)
            throw new Exception("Password verification failed.");

        string token = GenerateJwtToken(user);
        
        return token;
    }

    // TODO: добавить валидацию полей
    public async Task<bool> Register(RegisterUserRequest request)
    {
        // проверяем на уникальный email
        var user = await _userService.GetUserByEmailAsync(request.Email);
        
        if (user != null)
            throw new UserAlreadyExistsException(request.Email);
            
        string passwordHash = HashPassword(request.Password);
        return await _userService.CreateUserAsync(request.Email, passwordHash);
    }

    string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var passwordHash = passwordHasher.HashPassword(null, password);
        return passwordHash;
    }

    bool VerifyPassword(string passwordHash, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(null, passwordHash, password);
        return result == PasswordVerificationResult.Success;
    }

    string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        //TODO: скорее всего инжектнул дерьмово, так нельзя делать
        byte[] key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
        var securityKey = new SymmetricSecurityKey(key);

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256Signature
            )
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}