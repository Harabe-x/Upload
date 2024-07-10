using System.Security.Claims;
using ImageVault.ClassLibrary.Validation.Interfaces;
using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ImageVault.Server.Controllers;

[Route("api/auth")]
[Controller]
public class UserAuthenticationController : ControllerBase
{
    public UserAuthenticationController(IUserAuthenticationRepository userAuthenticationRepository,ITokenService tokenService)
    {
        _userAuthenticationRepository = userAuthenticationRepository;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    [EnableRateLimiting("register") ]
    public async Task<IActionResult> Register([FromBody] RegisterAccountDto accountData )
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");

            var accountRegistrationResult = await _userAuthenticationRepository.CreateAccount(accountData);

            if (!accountRegistrationResult.IsSuccess) return BadRequest("Invalid Data");

            var user = accountRegistrationResult.User;
            var authenticationResultDto =
                new AuthenticationResultDto(user.FirstName, user.Email, _tokenService.CreateToken(user));

            return Ok(authenticationResultDto);
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

            var loginResult = await  _userAuthenticationRepository.LoginUser(loginData);

            if (!loginResult.IsSuccess || loginResult.User == null) return Unauthorized("Login failed");

            var user = loginResult.User;    
            
            var loggedUser =
                new AuthenticationResultDto(user.FirstName, user.Email, _tokenService.CreateToken(user));

             return Ok(loggedUser);

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
        return Ok("Success");
    }
    
    private readonly ITokenService _tokenService; 
    
    private readonly IUserAuthenticationRepository _userAuthenticationRepository;
}
