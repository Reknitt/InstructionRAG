using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class ChatRepository(IDbContextFactory<SqliteDbContext> dbContextFactory) : IChatRepository
{
    private readonly IDbContextFactory<SqliteDbContext> _dbContextFactory = dbContextFactory;

    public async Task<Chat> GetByGuidAsync(Guid uuid)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        Chat? chat = await dbContext.Chats.Where(c => c.Id == uuid).FirstAsync();
        
        if (chat == null)
            throw new Exception("Chat with provided uuid does not exist");
        
        return chat;
    }

    public async Task<Chat> CreateAsync(Chat chat)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        Chat chatFromDb = dbContext.Chats.Add(chat).Entity;
        await dbContext.SaveChangesAsync();
        return chatFromDb;
    }

    public async Task UpdateAsync(Chat chat)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var chatFromDb = await dbContext.Chats.Where(e => e.Id == chat.Id).FirstOrDefaultAsync();
        
        if (chatFromDb == null)
            throw new Exception("Chat with provided uuid does not exist");
        
        dbContext.Entry(chatFromDb).CurrentValues.SetValues(chat);
        await dbContext.SaveChangesAsync();
    }
}