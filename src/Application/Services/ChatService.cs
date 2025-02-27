using System.Text;
using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Services;

public class ChatService(IChatRepository chatRepository) : IChatService
{
    private readonly IChatRepository _chatRepository = chatRepository;
   
    public async Task<Chat> GetChatAsync(Guid uuid)
    {
        var chat = await _chatRepository.GetByGuidAsync(uuid);
        return chat;
    }

    public async Task<Chat> CreateChatAsync(InitChatRequest request)
    {
        var chat = new Chat
        {
            Title = request.Title,
            Context = new string(""),
        };

        // Возможно стоит тут обработать исключение, хотя это можно сделать и выше уровнем
        Chat chatDb = await _chatRepository.CreateAsync(chat);
        return chatDb;
    }

    public Task<Chat> AddMessageToChatAsync(string chatId, string message)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Chat chat)
    {
        await _chatRepository.UpdateAsync(chat);
    }
}