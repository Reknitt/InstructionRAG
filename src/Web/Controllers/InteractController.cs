using System.Text;
using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/interact")]
public class InteractController(
    IModelService modelService,
    IChatService chatService
    ) : ControllerBase
{
    private readonly IModelService _modelService = modelService;
    private readonly IChatService _chatService = chatService;

    [HttpPut("init-model")]
    public async Task<IActionResult> InitModel()
    {
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
        var sbResponse = new StringBuilder();
        await foreach (var response in _modelService.QueryAsync(queryModelRequest))
        {
            Console.Write(response.Response);
            sbResponse.Append(response.Response);
        }

        return Ok(sbResponse.ToString());
    }
}