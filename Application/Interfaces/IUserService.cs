using InstructionRAG.Application.DTOs;
using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IUserService
{
    public Task<User?> GetUserByIdAsync(int id); 
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task<bool> CreateUserAsync(string username, string password);
    public Task UpdateUserAsync(User user);
}