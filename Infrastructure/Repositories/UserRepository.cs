using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class UserRepository(
    IDbContextFactory<SqliteDbContext> dbContextFactory, 
    ILogger<UserRepository> logger) : IUserRepository
{
    private readonly IDbContextFactory<SqliteDbContext> _dbContextFactory = dbContextFactory;
    private readonly ILogger<UserRepository> _logger = logger;
    
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

    public async Task<User?> GetByEmailAsync(string email)
    {
        await using var context  = await _dbContextFactory.CreateDbContextAsync();
        var query = context.Users.Where(u => u.Email == email);
        var user = await query.FirstOrDefaultAsync();
        return user;
    }

    public async Task<bool> CreateAsync(User user)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var userInDb = context.Users.Add(user);
        int count = await context.SaveChangesAsync();
        
        _logger.LogInformation($"info: User {user.Email} has been created");
        
        return count == 1;
    }

    public async Task UpdateAsync(User user)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email); 
        
        if (existingUser == null)
            throw new ArgumentException("User does not exist");
        
        context.Entry(existingUser).CurrentValues.SetValues(user);
        await context.SaveChangesAsync();
    }
}