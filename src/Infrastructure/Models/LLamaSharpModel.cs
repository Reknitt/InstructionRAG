using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using LLama;
using LLama.Common;
using LLama.Sampling;

namespace InstructionRAG.Infrastructure.Models;

public class LLamaSharpModel : IModel
{
    private readonly LLamaWeights _model;
    private readonly InteractiveExecutor _interactiveExecutor;
    public LLamaSharpModel(LLamaWeights model, LLamaContext context)
    {
        _model = model;
        _interactiveExecutor = new InteractiveExecutor(context);
    }

    /*
     * Должна быть какая-то прослойка, которая поддерживает id чата, сессий то может быть много и контекстов тоже,
     * пока обойдемся одним контекстом
     */
    public async IAsyncEnumerable<QueryModelResponse> ProccessTextAsync(string inputText)
    {
        var session = new ChatSession(_interactiveExecutor, new ChatHistory());

        var inferenceParams = new InferenceParams
        {
            SamplingPipeline = new DefaultSamplingPipeline
            {
                Temperature   = 0.6f
            },
            MaxTokens = -1,
            AntiPrompts = ["User:"]
        };
        Console.WriteLine($"User prompt: {inputText}"); 
        await foreach (var text in session.ChatAsync(
                           new ChatHistory.Message(AuthorRole.User, inputText), 
                           inferenceParams))
        {
            yield return new QueryModelResponse
            {
                Response = text
            };
        }
    }

    public ModelInfoResponse GetInfo()
    {
        var modelInfo = new ModelInfoResponse()
        {
            Name = "LLama Sharp model",
            CountOfParameters = _model.ParameterCount
        };

        return modelInfo;
    }
}