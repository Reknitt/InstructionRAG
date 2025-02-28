using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Database;
using InstructionRAG.Infrastructure.Repositories;

namespace InstructionRAG.Infrastructure;

public class UnitOfWork(
    ApplicationDbContext context
    ) : IDisposable
{

    private GenericRepository<Chat> chatRepository;
    private GenericRepository<User> userRepository;
    
    public GenericRepository<Chat> ChatRepository
    {
        get
        {
            if (chatRepository == null)
                chatRepository = new GenericRepository<Chat>(context);
            return chatRepository;
        }
    }

    public GenericRepository<User> UserRepository
    {
        get
        {
            if (userRepository == null)
                userRepository = new GenericRepository<User>(context);
            return userRepository;
        }
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }

    public async void RollbackAsync()
    {
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}