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
    private readonly ITokenService _tokenService;

    private readonly IUserAuthenticationRepository _userAuthenticationRepository;

    public UserAuthenticationController(IUserAuthenticationRepository userAuthenticationRepository,
        ITokenService tokenService)
    {
        _userAuthenticationRepository = userAuthenticationRepository;
        _tokenService = tokenService;
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
                ? Ok(new AuthenticationResultDto(accountRegistrationResult.User.Email,
                    _tokenService.CreateToken(accountRegistrationResult.User)))
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

            return loginResult.IsSuccess && loginResult.User is { }
                ? Ok(new AuthenticationResultDto(loginResult.User.Email, _tokenService.CreateToken(loginResult.User)))
                : BadRequest(loginResult.Error);
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

    [HttpGet("test")]
    [EnableRateLimiting("register")]
    public IActionResult Test()
    {
        return Ok($"api/auth/test {Random.Shared.Next(111, 1111)}");
    }
}