using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;

namespace InstructionRAG.Application.Services;

public class ModelService(IModelLoadingStrategy loadingStrategy) : IModelService
{
    private IModel Model { get; set; } 

    public async Task InitializeAsync()
    {
        Model = await loadingStrategy.LoadModelAsync();
    }
    public ModelInfoResponse GetInfo()
    {
        if (Model == null)
            throw new NullReferenceException("Model is null");
        return Model.GetInfo(); 
    }

    public async IAsyncEnumerable<QueryModelResponse> QueryAsync(QueryModelRequest query)
    {
        await foreach (var response in Model.ProccessTextAsync (query.Content))
        {
            Console.WriteLine(response.Response);
            yield return response;
        }
    }

    public async void InitializeChat()
    {
        
    }
}