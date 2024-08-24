using System.Security.Claims;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Data.Models;
using ImageVault.UserService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UserService.Controllers;

/// <summary>
///  Controller which is responsible for managing user data
/// </summary>
[Authorize]
[Controller]
[Route("/api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Gets user from the database 
    /// </summary>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<IActionResult>GetUserData()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userRepository.GetUser(id);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }

    /// <summary>
    ///  Creates a new user in the database
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserData userData)
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        if (User.GetClaimValue(ClaimTypes.Email) != userData.Email) return BadRequest(new Error("Invalid email"));

        try
        {
            var result = await _userRepository.AddUser(userData, id);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }

    /// <summary>
    /// Updates a user data in database
    /// </summary>
    /// <param name="newUserData"></param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser newUserData)
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userRepository.UpdateUser(newUserData, id);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }

    /// <summary>
    ///  Removes user from database 
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userRepository.DeleteUser(id);

            return result ? NoContent() : BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500, new Error("Internal server error"));
        }
    }
}