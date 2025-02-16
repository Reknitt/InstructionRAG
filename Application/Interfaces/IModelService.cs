using InstructionRAG.Application.DTOs;

namespace InstructionRAG.Application.Interfaces;

public interface IModelService
{
    Task InitializeAsync();
    ModelInfoResponse GetInfo();
    IAsyncEnumerable<QueryModelResponse> QueryAsync(QueryModelRequest query);
}