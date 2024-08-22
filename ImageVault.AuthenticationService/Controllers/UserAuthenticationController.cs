using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ImageVault.AuthenticationService.Controllers;

/// <summary>
/// A controller that handles authentication operations 
/// </summary>
[Route("api/auth")]
[Controller]
public class UserAuthenticationController : ControllerBase
{
    private readonly IUserAuthenticationRepository _userAuthenticationRepository;

    private readonly ILogger<UserAuthenticationController> _logger; 
    
    public UserAuthenticationController(IUserAuthenticationRepository userAuthenticationRepository, ILogger<UserAuthenticationController> logger)
    {
        _userAuthenticationRepository = userAuthenticationRepository;
        _logger = logger;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [EnableRateLimiting("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccount accountData)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(new Error("Invalid data"));

            var accountRegistrationResult = await _userAuthenticationRepository.CreateAccount(accountData);

            return accountRegistrationResult.IsSuccess
                ? Ok(accountRegistrationResult.Value)
                : BadRequest(accountRegistrationResult.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] Login loginData)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");

            var loginResult = await _userAuthenticationRepository.LoginUser(loginData);

            return loginResult.IsSuccess
                ? Ok(loginResult.Value)
                : Unauthorized(loginResult.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return Unauthorized("Login failed");
        }
    }

    [Authorize]
    [HttpGet("pingAuth")]
    [EnableRateLimiting("login")]
    public IActionResult PingAuth()
    {
        return Ok();
    }
}