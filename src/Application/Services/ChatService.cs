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

    public async Task<Chat> CreateChatAsync(Chat chat)
    {
        // Возможно стоит тут обработать исключение, хотя это можно сделать и выше уровнем
        Chat chatDb = await _chatRepository.CreateAsync(chat);
        return chatDb;
    }

    //TODO: сделать прослойку в виде буфера и хранить чат в памяти и только после некоторого времени писать в бд
    public async Task<Chat> AddMessageToChatAsync(Guid chatId, string message)
    {
        Chat chatDb = await _chatRepository.GetByGuidAsync(chatId);
        chatDb.Context += message;
        await _chatRepository.UpdateAsync(chatDb);
        return chatDb;
    }

    public async Task UpdateAsync(Chat chat)
    {
        await _chatRepository.UpdateAsync(chat);
    }
}