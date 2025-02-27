using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Infrastructure.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext 
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
}