using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IUserRepository
{
   public Task<IEnumerable<User>> GetAllAsync(); 
   public Task<User?> GetByIdAsync(int id);
   public Task<User?> GetByUsernameAsync(string username);
   public Task<bool> CreateAsync(User user);
}