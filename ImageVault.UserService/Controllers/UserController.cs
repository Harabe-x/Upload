using System;
using System.Security.Claims;
using System.Threading.Tasks;
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
[Controller]
[Route("/api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    private readonly ILogger<UserController> _logger;
    
    public UserController(IUserRepository userRepository, ILogger<UserController>  logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Gets user from the database 
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("get")]
    public async Task<IActionResult>GetUserData()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userRepository.GetUser(id);
            
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, new Error("Internal server error"));
        }
    }
    

    /// <summary>
    /// Updates a user data in database
    /// </summary>
    /// <param name="newUserData"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPatch("edit")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser newUserData)
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userRepository.UpdateUser(newUserData, id);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, new Error("Internal server error"));
        }
    }

    /// <summary>
    ///  Removes user from database 
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _userRepository.DeleteUser(id);

            return result ? NoContent() : BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, new Error("Internal server error"));
        }
    }
}