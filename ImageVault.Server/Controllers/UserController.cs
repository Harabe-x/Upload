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
    public UserController(IUserRepository userRepository,UserManager<ApplicationUser> userManager,ITokenService tokenService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserAccountDto accountData )
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");

            var registrationResult = await _userRepository.CreateAccount(accountData);

            if (!registrationResult.IsSuccess) return BadRequest("Invalid Data");

            var newUserDto = new NewUserDto
            {
                Email = registrationResult.User.Email,
                Name = registrationResult.User.FirstName, 
                Token = _tokenService.CreateToken(registrationResult.User)
            };

            return Ok(newUserDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }

    }

    private readonly ITokenService _tokenService; 
    
    private readonly IUserRepository _userRepository;

    private readonly UserManager<ApplicationUser> _userManager;
}