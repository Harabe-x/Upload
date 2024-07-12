using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Data.Models;
using ImageVault.UserService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ImageVault.UserService.Controllers;


[Authorize]
[Controller]
[Route("/api/account")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository; 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserData()
    {
        var id= User.GetClaimValue(ClaimTypes.NameIdentifier);
    
        try
        {
          var result= await  _userRepository.GetUser(id);

          return result.IsSuccess ? Ok(result.UserData) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDataDto userData)
    {
        var id= User.GetClaimValue(ClaimTypes.NameIdentifier);

        if (User.GetClaimValue(ClaimTypes.Email) !=  userData.Email) return BadRequest(new Error("Invalid email"));
        
        try
        {
            var result = await _userRepository.AddUser(userData, id);
            
            return result.IsSuccess ? Ok(result.UserData) : BadRequest(result.Error);
        }
        catch (Exception) 
        {
            return StatusCode(500, new Error("Internal server error"));
        }        
        
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateUserData([FromBody] UserDataDto newUserData)
    {
        var id  = User.GetClaimValue(ClaimTypes.NameIdentifier);
        
        try
        {
            var result= await  _userRepository.UpdateUser(newUserData,id );
    
            return result.IsSuccess ? Ok(result.UserData) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);
    
        try
        {
            var result= await  _userRepository.DeleteUser(id);
    
            return result ? NoContent() : BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }
    
}