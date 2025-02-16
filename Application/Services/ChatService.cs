using System.Text;
using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;

namespace InstructionRAG.Application.Services;

public class ChatService(IChatRepository chatRepository) : IChatService
{
    private readonly IChatRepository _chatRepository = chatRepository;
   
    public async Task<Chat> GetChatAsync(string uuid)
    {
        var chat = await _chatRepository.GetByGuidAsync(uuid);
        return chat;
    }

    public async Task<string> CreateChatAsync(InitChatRequest request)
    {
        var uuid = Guid.NewGuid().ToString();

        var chat = new Chat
        {
            Title = request.Title,
            ChatId = uuid,
            Context = new string(""),
        };
        
        var res = await _chatRepository.CreateAsync(chat);
        
        if (res == 0)
            throw new ApplicationException("Failed to create chat");
        
        return uuid;
    }

    public Task<Chat> AddMessageToChatAsync(string chatId, string message)
    {
        throw new NotImplementedException();
    }
}