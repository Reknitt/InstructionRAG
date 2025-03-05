using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Domain.Exceptions;
using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class UserRepository(
    ApplicationDbContext context, 
    ILogger<UserRepository> logger) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<UserRepository> _logger = logger;
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // await using var context = await _dbContext.CreateDbContextAsync();
        var users = _context.Users.ToList(); 
        return users ?? throw new UserException("The table Users is empty.");
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var query = _context.Users.Where(u => u.Email == email);
        var user = await query.FirstOrDefaultAsync();
        return user;
    }

    public async Task<bool> CreateAsync(User user)
    {
        var userInDb = _context.Users.Add(user);
        int count = await _context.SaveChangesAsync();
        
        _logger.LogInformation($"info: User {user.Email} has been created");
        
        return count == 1;
    }

    public async Task UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email); 
        
        if (existingUser == null)
            throw new UserNotFoundException(user.Email);
        
        _context.Entry(existingUser).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
    }
}