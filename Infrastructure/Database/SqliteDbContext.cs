using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InstructionRAG.Infrastructure.Database;

public class SqliteDbContext(IOptions<SqliteDatabaseConfig> dbConfig) : IdentityDbContext <User>
{
    private readonly SqliteDatabaseConfig _dbConfig = dbConfig.Value;
    
    public DbSet<User> Users { get; set; } 
    public DbSet<Chat> Chats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite(_dbConfig.ConnectionString);
    }
}