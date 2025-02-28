using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InstructionRAG.Infrastructure.Database;

// TODO: можно отрефакторить, чтобы был универсальный dbConfig т.к. там все равно только constring
public class PostgresDbContext(IOptions<PostgresDbConfig> options) : DbContext
{
    private readonly PostgresDbConfig _dbConfig = options.Value;
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_dbConfig.ConnectionString);
    }
}