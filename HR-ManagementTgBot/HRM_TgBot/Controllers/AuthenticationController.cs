using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_TgBot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IJwtAuthenticationService _authenticationService;

    public AuthenticationController(IJwtAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Auth")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] string authenticationKey)
    {
        try
        {
            return Ok(await _authenticationService.LogInAsync(authenticationKey));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Wrong credentials");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}