using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;

[Route("api/account")]
[Controller]
public class UserController : ControllerBase
{
    public UserController(IUserRepository userRepository,UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _userRepository = userRepository; 
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserAccountDto accountData )
    {
        try
        {
            if (!ModelState.IsValid || !await _userRepository.CreateAccount(accountData))
            {
                return BadRequest();
            }

            return Ok("Successfully registered");
        }
        catch (Exception e)
        {
            return BadRequest("An error occured");
        }

    }


    private readonly IUserRepository _userRepository;

    private readonly UserManager<ApplicationUser> _userManager;
}