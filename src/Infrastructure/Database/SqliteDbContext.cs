using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InstructionRAG.Infrastructure.Database;

public class SqliteDbContext(IOptions<SqliteDatabaseConfig> dbConfig) : ApplicationDbContext 
{
    private readonly SqliteDatabaseConfig _dbConfig = dbConfig.Value;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite(_dbConfig.ConnectionString);
    }

    
}