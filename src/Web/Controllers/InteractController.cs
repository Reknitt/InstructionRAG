using System.Text;
using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/interact")]
public class InteractController(
    IModelService modelService,
    IChatService chatService,
    ILogger<InteractController> logger
    ) : ControllerBase
{
    private readonly IModelService _modelService = modelService;
    private readonly IChatService _chatService = chatService;
    private readonly ILogger<InteractController> _logger = logger;

    [HttpPut("init-model")]
    public async Task<IActionResult> InitModel()
    {
        _logger.LogInformation("Init model");
        await _modelService.InitializeAsync();
        return Ok("model initialized");
    }

    [HttpGet("model-info")]
    public ActionResult GetModelInfo()
    {
        return Ok(_modelService.GetInfo());
    }

    [HttpPost("query-model")]
    public async Task<IActionResult> QueryModel([FromBody] QueryModelRequest queryModelRequest)
    {
        _logger.LogInformation("Query model");
        
        var chatId = queryModelRequest.ChatId;
        string userMessage = "User: " + queryModelRequest.Content + "\nBot: ";
        await _chatService.AddMessageToChatAsync(chatId, userMessage);
        
        await foreach (var response in _modelService.QueryAsync(queryModelRequest))
        {
            await _chatService.AddMessageToChatAsync(chatId, response.Response);
        }

        return Ok(chatId);
    }
}