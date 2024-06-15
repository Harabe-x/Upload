using System.Security.Claims;
using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Enums;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.ExtensionMethods;
using ImageVault.Server.Models;
using ImageVault.Server.Repository;
using ImageVault.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;

[Route("api/account")]
[Controller]
public class UserController : ControllerBase
{
    public UserController(IUserAuthenticationRepository userAuthenticationRepository,ITokenService tokenService,IUserRepository userRepository)
    {
        _userAuthenticationRepository = userAuthenticationRepository;
        _userRepository = userRepository; 
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountDto accountData )
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
    
    [HttpPost("login")]
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

    [Authorize]
    [HttpGet("GetUserData")]
    public async Task<IActionResult> GetUserData([FromHeader]string token)
    {
        var tokenDto  =   _tokenService.ValidateToken(token);

        if (!tokenDto.IsSuccess) return Unauthorized();

        var email = tokenDto.Claims.GetClaimValue(ClaimTypes.Email);

        if (email == null) return StatusCode(500, "Internal Server Error");

        var userData = await _userRepository.GetUserData(email);

        return Ok(userData);
    }
   
    [HttpDelete("DeleteUser")]
    [Authorize]
    public async Task<IActionResult> DeleteUser([FromBody]string token)
    {
        var tokenDto  =   _tokenService.ValidateToken(token);

        if (!tokenDto.IsSuccess) return Unauthorized();

        var email = tokenDto.Claims.GetClaimValue(ClaimTypes.Email);

        if (email == null) return StatusCode(500, "Internal Server Error");

        var result = await _userRepository.DeleteUser(email);

        return result ? Ok("Success") : BadRequest("Error"); 
    }
    
    
    [HttpPatch("UpdateUser")]
    [Authorize]
    public async Task<IActionResult> DeleteUser([FromBody]UpdateUserProfileDto profileDto,[FromHeader]string token)
    {
        var tokenDto  =   _tokenService.ValidateToken(token);

        if (!tokenDto.IsSuccess) return Unauthorized();

        var email = tokenDto.Claims.GetClaimValue(ClaimTypes.Email);

        if (email == null) return StatusCode(500, "Internal Server Error");

        var result = await _userRepository.UpdateUserData(profileDto, email);

        return result ? Ok("Success") : BadRequest("Error"); 
    }


    private readonly ITokenService _tokenService; 
    
    private readonly IUserAuthenticationRepository _userAuthenticationRepository;

    private IUserRepository _userRepository; 
}
