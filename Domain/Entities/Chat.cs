using System.Text;

namespace InstructionRAG.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public StringBuilder Context { get; set; }
}