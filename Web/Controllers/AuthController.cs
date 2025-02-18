using InstructionRAG.Application.DTOs;
using InstructionRAG.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var res = await _authService.Register(request);
        return res ? Ok(res) : BadRequest(res);
    } 
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var res = await _authService.Login(request);
        return res ? Ok(res) : Unauthorized();
    }
}