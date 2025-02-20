using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/chats")]
[Authorize]
public class ChatController(IChatService chatService) : ControllerBase
{
    private readonly IChatService _chatService = chatService;
     
    [HttpGet("init-chat")]
    public async Task<IActionResult> InitChat([FromQuery] InitChatRequest request)
    {
        var uuid = await _chatService.CreateChatAsync(request);
        
        return Ok(new
        {
            chatId = uuid
        });
    }

    [HttpGet("get-chat")]
    public async Task<IActionResult> GetChat([FromQuery] GetChatRequest request)
    {
        Chat chat = await _chatService.GetChatAsync(request.Uuid);
        return Ok(chat);
    }
}