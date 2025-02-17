namespace InstructionRAG.Application.DTOs;

[Obsolete("Старое DTO, больше не используется")]
public class RegisterUserRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}