using InstructionRAG.Application.DTOs;

namespace InstructionRAG.Application.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginUserRequest request);
    Task<bool> Register(RegisterUserRequest request);
}