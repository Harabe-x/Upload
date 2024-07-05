using System.Security.Claims;
using ImageVault.Server.Data.Dtos.UserDtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Interfaces.User;
using ImageVault.Server.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;


[Controller]
[Route("/api/account")]
[Authorize]
public class UserController : ControllerBase
{
    
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository; 
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserData()
    {
        var email = User.GetClaimValue(ClaimTypes.Email);

        try
        {
          var result= await  _userRepository.GetUser(email);

          if (!result.IsSuccess) return BadRequest();

          return Ok(result.UserData);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUserData([FromBody] UserDataDto newUserData)
    {
        var email = User.GetClaimValue(ClaimTypes.Email);

        try
        {
            var result= await  _userRepository.UpdateUser(newUserData,email);

            if (!result.IsSuccess) return BadRequest();

            return Ok(result.UserData);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        var email = User.GetClaimValue(ClaimTypes.Email);

        try
        {
            var result= await  _userRepository.DeleteUser(email);

            if (!result) return BadRequest();

            return NoContent(); 
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    private readonly IUserRepository _userRepository;
}