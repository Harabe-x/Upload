using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ApiKeyService.Controllers;

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

    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> AddKey([FromBody] AddApiKeyDto apiKeyDto)
    {
        if (apiKeyDto == null)
            return BadRequest(new Error("Api key data can't be empty"));

        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

         try
        {
            var result = await _apiKeyRepository.AddKey(apiKeyDto, id);

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

    [HttpPatch("editKey")]
    [Authorize]
    public async Task<IActionResult> UpdateKey([FromBody] EditApiKeyDto newApiKeyData)
    {
        if (string.IsNullOrEmpty(newApiKeyData.Key) || newApiKeyData == null)
            return BadRequest(new Error("Api key can't be empty"));

        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);
        
        try
        {
            var result = await _apiKeyRepository.EditKey(newApiKeyData,  id, newApiKeyData.Key);

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