using InstructionRAG.Application.DTOs;
using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

// TODO: убрать Chat из названий
public interface IChatService
{
    Task<Chat> GetChatAsync(Guid uuid);
    Task<Chat> CreateChatAsync(Chat request);
    Task<Chat> AddMessageToChatAsync(Guid chatId, string message);
    Task UpdateAsync(Chat chat);
}