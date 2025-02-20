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

    public async Task<Guid> CreateAsync(Chat chat)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        // await dbContext.Database.EnsureCreatedAsync();
        Chat chatFromDb = dbContext.Chats.Add(chat).Entity;
        await dbContext.SaveChangesAsync();
        return chatFromDb.Id;
    }

    public Task<Chat> UpdateAsync(Chat chat)
    {
        throw new NotImplementedException();
    }
}