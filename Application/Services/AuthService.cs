using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Application.Services;

public class AuthService(IUserService userService, ILogger<AuthService> logger) : IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly ILogger _logger = logger;
    
    /* TODO: по-моему опять напутал че-то с возвращаемым типом
     *       надо по идее кидать исключения 
    */ 
    public async Task<bool> Login(LoginUserRequest request)
    {
        User? user = await _userService.GetUserByEmailAsync(request.Email);
        
        if (user == null)
        {
            _logger.LogWarning("User with provided email: {Email} does not exist.", request.Email); 
            return false;
        }
        
        
        _logger.LogInformation("User with email: {Email} was found.", request.Email);
        var passwordHasher = new PasswordHasher<User>();
        
        if (user.PasswordHash == null) return false;
        
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        
        _logger.LogWarning("Result of password verification is {Result}", result); 
        
        return result == PasswordVerificationResult.Success;
    }

    // TODO: добавить валидацию полей
    public async Task<bool> Register(RegisterUserRequest request)
    {
        // проверяем на уникальный email
        var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null)
            return await _userService.CreateUserAsync(request.Email, request.Password);
        return false;
    }
}