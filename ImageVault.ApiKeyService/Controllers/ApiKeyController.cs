using System.Security.Claims;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ApiKeyService.Controllers;


/// <summary>
///  Controller that allows the user to manage API keys
/// </summary>
[Controller]
[Route("/api/apikey")]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyRepository _apiKeyRepository;

    private readonly ILogger<ApiKeyController> _logger;

    public ApiKeyController(IApiKeyRepository apiKeyRepository, ILogger<ApiKeyController> logger)
    {
        _apiKeyRepository = apiKeyRepository;
        _logger = logger;
    }

    /// <summary>
    ///  Gets data about specified API key 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpPost("get")]
    [Authorize]
    public async Task<IActionResult> GetKey([FromBody] string key)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest(new Error("Api key can't be empty"));

        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);
        try
        {
            var result = await _apiKeyRepository.GetKey(key, id);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(
                $"[{DateTime.Now}] Error occured | Message : {e.Message} | InnerException : {e.InnerException}");

            return StatusCode(500, new Error("Internal server error. We will try to fix it as soon as possible"));
        }
    }
    
    /// <summary>
    ///  Gets all API keys created by user
    /// </summary>
    /// <returns>All user API keys</returns>
    [HttpPost("getKeys")]
    [Authorize]
    public async Task<IActionResult> GetKeys()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);
        try
        {
            var result = await _apiKeyRepository.GetKeys(id);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(
                $"[{DateTime.Now}] Error occured | Message : {e.Message} | InnerException : {e.InnerException}");
            return StatusCode(500, new Error("Internal server error. We will try to fix it as soon as possible"));
        }
    }

    /// <summary>
    ///  Adds API key to the database
    /// </summary>
    /// <param name="apiKey"></param>
    /// <returns>Added API key</returns>
    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> AddKey([FromBody] AddApiKey apiKey)
    {
        if (apiKey == null)
            return BadRequest(new Error("Api key data can't be empty"));

        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _apiKeyRepository.AddKey(apiKey, id);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(
                $"[{DateTime.Now}] Error occured | Message : {e.Message} | InnerException : {e.InnerException}");

            return StatusCode(500, new Error("Internal server error. We will try to fix it as soon as possible"));
        }
    }

    /// <summary>
    ///  Edits API key in database
    /// </summary>
    /// <param name="newApiKeyData"></param>
    /// <returns></returns>
    [HttpPatch("editKey")]
    [Authorize]
    public async Task<IActionResult> UpdateKey([FromBody] EditApiKey newApiKeyData)
    {
        if (string.IsNullOrEmpty(newApiKeyData.Key) || newApiKeyData == null)
            return BadRequest(new Error("Api key can't be empty"));

        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var result = await _apiKeyRepository.EditKey(newApiKeyData, id, newApiKeyData.Key);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(
                $"[{DateTime.Now}] Error occured | Message : {e.Message} | InnerException : {e.InnerException}");
            return StatusCode(500, new Error("Internal server error. We will try to fix it as soon as possible"));
        }
    }

    /// <summary>
    ///  Deletes API key from database
    /// </summary>
    /// <param name="key">Key to delete</param>
    /// <returns></returns>
    [HttpDelete("deleteKey")]
    [Authorize]
    public async Task<IActionResult> DeleteKey([FromBody] string key)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest(new Error("Api key can't be empty"));

        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);
        try
        {
            var result = await _apiKeyRepository.DeleteKey(key, id);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(
                $"[{DateTime.Now}] Error occured | Message : {e.Message} | InnerException : {e.InnerException}");

            return StatusCode(500, new Error("Internal server error. We will try to fix it as soon as possible"));
        }
    }
}