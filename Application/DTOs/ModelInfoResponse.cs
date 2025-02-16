namespace InstructionRAG.Application.DTOs;

public class ModelInfoResponse
{
    public required string Name { get; set; }
    public required ulong CountOfParameters { get; set; }
}