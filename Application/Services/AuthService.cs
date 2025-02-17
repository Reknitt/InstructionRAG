using InstructionRAG.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Application.Services;

public class AuthService(IUserService userService) : IAuthService
{
    private readonly IUserService _userService = userService;
    
    public Task<bool> Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Register(RegisterRequest request)
    {
        return await _userService.CreateUserAsync(request.Email, request.Password);
    }
}