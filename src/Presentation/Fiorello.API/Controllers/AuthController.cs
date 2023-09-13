using Fiorello.Application.Abstraction.Services;
using Fiorello.Application.DTOs.AuthDtos;
using Fiorello.Persistence.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiorello.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto user)
    {
        try
        {
            await _authService.RegisterAsync(user);
            return Ok();
        }
        catch (SignUpException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = ex.Message,
            });
        }
    }

}
