using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Application.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginRequest request);
    Task<bool> Register(RegisterRequest request);
}