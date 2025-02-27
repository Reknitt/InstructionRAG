using InstructionRAG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstructionRAG.Application.Interfaces;

public interface IApplicationDbContext 
{
        public DbSet<User> Users { get; set; } 
        public DbSet<Chat> Chats { get; set; }
}