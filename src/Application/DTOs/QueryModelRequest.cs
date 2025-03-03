namespace InstructionRAG.Application.DTOs;

public class QueryModelRequest
{
    public required string Content { get; set; }
    public required Guid ChatId {get; set;}
}