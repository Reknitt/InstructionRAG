using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstructionRAG.Web.Controllers;

[ApiController]
[Route("api/testauth")]
[Authorize]
public class TestAuthController : ControllerBase
{
    [HttpGet]
    public IActionResult TestAuth()
    {
        return Ok("Если вы видите это сообщение, то вы авторизованы");
    }
}