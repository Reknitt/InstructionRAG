using InstructionRAG.Application.DTOs;
using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Interfaces;

public interface IChatService
{
    Task<Chat> GetChatAsync(Guid uuid);
    Task<string> CreateChatAsync(InitChatRequest request);
    Task<Chat> AddMessageToChatAsync(string chatId, string message);
    
}