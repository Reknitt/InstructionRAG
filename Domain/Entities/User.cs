using Microsoft.AspNetCore.Identity;

namespace InstructionRAG.Domain.Entities;

// TODO: заюзать Identity Core
public class User : IdentityUser
{  
    // public int Id { get; set; }
    // public string Username { get; set; }
    // public string HashedPassword { get; set; }
    public Chat[] Chats { get; set; }
}