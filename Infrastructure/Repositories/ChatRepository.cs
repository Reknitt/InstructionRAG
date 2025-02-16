using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class ChatRepository(IDbContextFactory<SqliteDbContext> dbContextFactory) : IChatRepository
{
    private readonly IDbContextFactory<SqliteDbContext> _dbContextFactory = dbContextFactory;

    public async Task<Chat> GetByGuidAsync(string uuid)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var chat = await dbContext.Chats.Where(c => c.ChatId == uuid).FirstAsync();
        
        if (chat == null)
            throw new Exception("Chat with provided uuid does not exist");
        
        return chat;
    }

    public async Task<int> CreateAsync(Chat chat)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        // await dbContext.Database.EnsureCreatedAsync();
        dbContext.Chats.Add(chat);
        return await dbContext.SaveChangesAsync();
    }

    public Task<Chat> UpdateAsync(Chat chat)
    {
        throw new NotImplementedException();
    }
}