using InstructionRAG.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Repositories;

public class GenericRepository<TEntity>(ApplicationDbContext context)
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> Get()
    {
        return await _dbSet.ToListAsync();  
    }

    public async Task<TEntity?> GetById(object? id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Add(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }
}