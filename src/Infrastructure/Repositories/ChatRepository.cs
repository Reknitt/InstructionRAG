using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class ChatRepository(ApplicationDbContext dbContext) : IChatRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Chat> GetByGuidAsync(Guid uuid)
    {
        Chat? chat = await _dbContext.Chats.Where(c => c.Id == uuid).FirstAsync();
        
        if (chat == null)
            throw new ArgumentException("Chat with provided uuid does not exist");
        
        return chat;
    }

    public async Task<Chat> CreateAsync(Chat chat)
    {
        Chat chatFromDb = _dbContext.Chats.Add(chat).Entity;
        await _dbContext.SaveChangesAsync();
        return chatFromDb ?? throw new ApplicationException("Failed to add chat.");
    }

    public async Task UpdateAsync(Chat chat)
    {
        var chatFromDb = await _dbContext.Chats.Where(e => e.Id == chat.Id).FirstOrDefaultAsync();
        
        if (chatFromDb == null)
            throw new Exception("Chat with provided uuid does not exist");
        
        _dbContext.Entry(chatFromDb).CurrentValues.SetValues(chat);
        await _dbContext.SaveChangesAsync();
    }
}