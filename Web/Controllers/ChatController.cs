using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatController(IChatService chatService) : ControllerBase
{
    private readonly IChatService _chatService = chatService;
     
    [HttpGet("init-chat")]
    public async Task<IActionResult> InitChat([FromQuery] InitChatRequest request)
    {
        var uuid = await _chatService.CreateChatAsync(request);
        var response = new InitChatResponse
        {
            Uuid = uuid
        };
        return Ok(response);
    }
}