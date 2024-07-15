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
    public async Task<IActionResult> GetValue()
    {
        var id= User.GetClaimValue(ClaimTypes.NameIdentifier);
    
        try
        {
          var result= await  _userRepository.GetUser(id);

          return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDataDto Value)
    {
        var id= User.GetClaimValue(ClaimTypes.NameIdentifier);

        if (User.GetClaimValue(ClaimTypes.Email) !=  Value.Email) return BadRequest(new Error("Invalid email"));
        
        try
        {
            var result = await _userRepository.AddUser(Value, id);
            
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception) 
        {
            return StatusCode(500, new Error("Internal server error"));
        }        
        
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateValue([FromBody] UserDataDto newValue)
    {
        var id  = User.GetClaimValue(ClaimTypes.NameIdentifier);
        
        try
        {
            var result= await  _userRepository.UpdateUser(newValue,id );
    
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
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