using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.Models;
using ImageVault.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;

[Route("api/account")]
[Controller]
public class UserController : ControllerBase
{
    public UserController(IUserAuthenticationRepository userAuthenticationRepository,UserManager<ApplicationUser> userManager,ITokenService tokenService)
    {
        _userManager = userManager;
        _userAuthenticationRepository = userAuthenticationRepository;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserAccountDto accountData )
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");

            var accountRegistrationResult = await _userAuthenticationRepository.CreateAccount(accountData);

            if (!accountRegistrationResult.IsSuccess) return BadRequest("Invalid Data");

            var authenticationResultDto = new AuthenticationResultDto
            {
                Email = accountRegistrationResult.User.Email,
                Name = accountRegistrationResult.User.FirstName, 
                Token = _tokenService.CreateToken(accountRegistrationResult.User)
            };

            return Ok(authenticationResultDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginData)
    {
        try
        { 
            var loginResult = await  _userAuthenticationRepository.LoginUser(loginData);

            if (!loginResult.IsSuccess || loginResult.User == null) return Unauthorized("Login failed");

             var loggedUser =  new AuthenticationResultDto
            {
                Email = loginResult.User.Email,
                Name = loginResult.User.FirstName,
                Token = _tokenService.CreateToken(loginResult.User)
            };

             return Ok(loggedUser);

        }
        catch (Exception e)
        {
            return Unauthorized("Login failed");
        }
        
        
    }

    private readonly ITokenService _tokenService; 
    
    private readonly IUserAuthenticationRepository _userAuthenticationRepository;

    private readonly UserManager<ApplicationUser> _userManager;
}