using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IUserRepository
{
   public Task<IEnumerable<User>> GetAllAsync(); 
   public Task<User?> GetByIdAsync(int id);
   public Task<User?> GetByEmailAsync(string email);
   public Task<bool> CreateAsync(User user);
}