using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InstructionRAG.Infrastructure.Database;

// TODO: можно отрефакторить, чтобы был универсальный dbConfig т.к. там все равно только conn string
public class PostgresDbContext(IOptions<PostgresDbConfig> dbConfig) : ApplicationDbContext
{
    private readonly PostgresDbConfig _dbConfig = dbConfig.Value;
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_dbConfig.ConnectionString);
    }
}