using InstructionRAG.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Application.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginUserRequest request);
    Task<bool> Register(RegisterUserRequest request);
}