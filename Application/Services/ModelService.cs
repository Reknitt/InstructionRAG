using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;

namespace InstructionRAG.Application.Services;

public class ModelService(IModelLoadingStrategy loadingStrategy) : IModelService
{
    private IModel _model;

    public async Task InitializeAsync()
    {
        _model = await loadingStrategy.LoadModelAsync();
    }
    public ModelInfoResponse GetInfo()
    {
        if (_model == null)
            throw new NullReferenceException("Model is null");
        return _model.GetInfo(); 
    }

    public async IAsyncEnumerable<QueryModelResponse> QueryAsync(QueryModelRequest query)
    {
        await foreach (var response in _model.ProccessTextAsync (query.Content))
        {
            yield return response;
        }
    }

    public async void InitializeChat()
    {
        
    }
}