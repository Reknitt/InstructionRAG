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
    IUserService userService,
    ILogger<ChatController> logger
    ) : ControllerBase
{
    private readonly IChatService _chatService = chatService;
    private readonly IUserService _userService = userService;
    private readonly ILogger<ChatController> _logger = logger;
     
    [HttpGet("init-chat")]
    public async Task<IActionResult> InitChat([FromQuery] InitChatRequest request)
    {
        User? user = await _userService.GetUserByEmailAsync(request.Email);

        // по идее не может быть user == null надо будет проследить это все
        if (user is null)
        {
            _logger.LogInformation("User with provided email: {email} was not found", user.Email);
            return Unauthorized("User was not found");
        }

        _logger.LogInformation("Initializing chat for user with email: {email}", user.Email);
        var chat = new Chat()
        {
            Title = request.Title,
            User = user,
            Context = "",
        };
        
        await _chatService.CreateChatAsync(chat);
        
        //await _chatService.UpdateAsync(chat);
         
        return Ok(new
        {
            chatId = chat.Id.ToString(),
            chatTitle = chat.Title,
            userId = user.Id.ToString(),
            userEmail = user.Email
        });
    }

    [HttpGet("get-chat")]
    public async Task<IActionResult> GetChat([FromQuery] GetChatRequest request)
    {
        Chat chat = await _chatService.GetChatAsync(request.Uuid);
        return Ok(chat);
    }
}