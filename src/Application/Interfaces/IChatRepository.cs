using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IChatRepository
{
    Task<Chat> GetByGuidAsync(Guid uuid);    
    Task<Chat> CreateAsync(Chat chat);
    Task UpdateAsync(Chat chat);
}