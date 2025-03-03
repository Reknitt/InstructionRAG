using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using InstructionRAG.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InstructionRAG.Infrastructure.Database;

public class ApplicationDbContext(IOptions<ApplicationDbConfig> options) : DbContext, IApplicationDbContext 
{
    ApplicationDbConfig _dbConfig = options.Value;
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        switch (_dbConfig.Provider)
        {
            case "PostgreSQL": 
                optionsBuilder.UseNpgsql(_dbConfig.ConnectionString);
                break;
            default:
                optionsBuilder.UseSqlite("Data Source=database.db");
                break;
        }
    }
}