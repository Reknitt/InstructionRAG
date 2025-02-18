using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Application.Services;

public class AuthService(IUserService userService) : IAuthService
{
    private readonly IUserService _userService = userService;
    
    /* TODO: по-моему опять напутал че-то с возвращаемым типом
     *       надо по идее кидать исключения 
    */ 
    public async Task<bool> Login(LoginRequest request)
    {
        User? user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null)
            return false;
        
        var passwordHasher = new PasswordHasher<User>();
        
        if (user.PasswordHash == null) return false;
        
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
 
        return result == PasswordVerificationResult.Success;
    }

    // TODO: добавить валидацию полей
    public async Task<bool> Register(RegisterRequest request)
    {
        // проверяем на уникальный email
        var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null)
            return await _userService.CreateUserAsync(request.Email, request.Password);
        return false;
    }
}