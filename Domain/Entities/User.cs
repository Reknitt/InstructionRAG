using Microsoft.AspNetCore.Identity;

namespace InstructionRAG.Domain.Entities;

// TODO: заюзать Identity Core
public class User 
{  
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Chat[] Chats { get; set; }
}