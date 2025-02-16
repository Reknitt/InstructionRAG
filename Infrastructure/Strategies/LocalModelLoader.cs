using InstructionRAG.Application.Interfaces;
using InstructionRAG.Infrastructure.Config;
using LLama;
using LLama.Common;
using InstructionRAG.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace InstructionRAG.Infrastructure.Strategies;

public class LocalModelLoader(IOptions<ModelConfig> modelConfig) : IModelLoadingStrategy
{
    private readonly ModelConfig _modelConfig = modelConfig.Value;

    public async Task<IModel> LoadModelAsync()
    {
        var parameters = new ModelParams(_modelConfig.ModelSource);
        var model = await LLamaWeights.LoadFromFileAsync(parameters);
        var context = model.CreateContext(parameters);
        return new LLamaSharpModel(model, context);
    }
}