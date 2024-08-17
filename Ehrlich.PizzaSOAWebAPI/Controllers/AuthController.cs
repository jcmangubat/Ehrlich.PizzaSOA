using Ehrlich.PizzaSOA.WebAPI.Models;
using Ehrlich.PizzaSOA.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.PizzaSOA.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _tokenService;

    public AuthController(JwtTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // Validate the user's credentials (this is a simplified example)
        if (login.Username == "test" && login.Password == "password") // Replace with actual validation
        {
            var token = _tokenService.GenerateToken(login.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}