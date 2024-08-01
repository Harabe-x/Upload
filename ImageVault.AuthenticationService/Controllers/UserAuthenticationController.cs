using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ImageVault.AuthenticationService.Controllers;

[Route("api/auth")]
[Controller]
public class UserAuthenticationController : ControllerBase
{
    private readonly IUserAuthenticationRepository _userAuthenticationRepository;

    public UserAuthenticationController(IUserAuthenticationRepository userAuthenticationRepository,
        ITokenService tokenService)
    {
        _userAuthenticationRepository = userAuthenticationRepository;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [EnableRateLimiting("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountDto accountData)
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
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginData)
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