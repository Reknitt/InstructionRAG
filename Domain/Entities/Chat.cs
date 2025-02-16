using System.Text;

namespace InstructionRAG.Domain.Entities;

public class Chat
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ChatId { get; set; }
    public string Context { get; set; }
}