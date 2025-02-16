using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IChatRepository
{
    Task<Chat> GetByGuidAsync(string uuid);    
    Task<int> CreateAsync(Chat chat);
    Task<Chat> UpdateAsync(Chat chat);
}