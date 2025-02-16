using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class UserRepository(IDbContextFactory<SqliteDbContext> dbContextFactory) : IUserRepository
{
    private readonly IDbContextFactory<SqliteDbContext> _dbContextFactory = dbContextFactory;
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var users = context.Users.ToList(); 
        return users;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(User user)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Users.Add(user);
        int count = await context.SaveChangesAsync();
        return count == 1;

    }
}