namespace InstructionRAG.Application.DTOs;

public class RegisterUserRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}