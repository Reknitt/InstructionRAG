using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IChatRepository
{
    Task<Chat> GetByGuidAsync(Guid uuid);    
    Task<Guid> CreateAsync(Chat chat);
    Task<Chat> UpdateAsync(Chat chat);
}