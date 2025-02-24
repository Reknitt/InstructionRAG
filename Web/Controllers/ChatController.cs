using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/chats")]
[Authorize]
public class ChatController(
    IChatService chatService,
    IUserService userService
    ) : ControllerBase
{
    private readonly IChatService _chatService = chatService;
    private readonly IUserService _userService = userService;
     
    [HttpGet("init-chat")]
    public async Task<IActionResult> InitChat([FromQuery] InitChatRequest request)
    {
        Chat chat = await _chatService.CreateChatAsync(request);
        User? user = await _userService.GetUserByEmailAsync(request.Email);

        // по идее не может быть user == null надо будет проследить это все
        if (user is null)
            return Unauthorized();

        chat.User = user;
        await _chatService.UpdateAsync(chat);
         
        return Ok(new
        {
            chatId = chat.Id.ToString()
        });
    }

    [HttpGet("get-chat")]
    public async Task<IActionResult> GetChat([FromQuery] GetChatRequest request)
    {
        Chat chat = await _chatService.GetChatAsync(request.Uuid);
        return Ok(chat);
    }
}