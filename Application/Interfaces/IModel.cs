using InstructionRAG.Application.DTOs;

namespace InstructionRAG.Application.Interfaces;

public interface IModel
{
    IAsyncEnumerable<QueryModelResponse> ProccessTextAsync(string inputText);
    ModelInfoResponse GetInfo();
}