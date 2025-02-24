using InstructionRAG.Application.DTOs;
using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

// TODO: убрать Chat из названий
public interface IChatService
{
    Task<Chat> GetChatAsync(Guid uuid);
    Task<Chat> CreateChatAsync(InitChatRequest request);
    Task<Chat> AddMessageToChatAsync(string chatId, string message);
    Task UpdateAsync(Chat chat);
}