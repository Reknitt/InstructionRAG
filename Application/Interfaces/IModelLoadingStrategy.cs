namespace InstructionRAG.Application.Interfaces;

public interface IModelLoadingStrategy
{
    Task<IModel> LoadModelAsync();
}