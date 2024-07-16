using System.Security.Claims;
using ImageVault.UserService.Data.Dtos.ApiKey;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UserService.Controllers;

[Controller]
[Route("/api/apikey")]
[Authorize]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyRepository _apiKeyRepository; 
    
    private readonly  string? _id = String.Empty;

    private readonly ILogger<ApiKeyController> _logger; 

    public ApiKeyController(IApiKeyRepository apiKeyRepository, ILogger<ApiKeyController> logger)
    {
        _apiKeyRepository = apiKeyRepository;
        _logger = logger; 
        _id = User.GetClaimValue(ClaimTypes.NameIdentifier);
    }
    
    [HttpPost]
    [Route("/api/apikey/getKey")]
    public async Task<IActionResult> GetApiKey([FromBody] string key)
    {
        try
        {
            var result = await _apiKeyRepository.GetApiKey(key,_id);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error"); 
;        }
    }
    
    [HttpPost]
    [Route("/api/apikey/getKeys")]
    public async Task<IActionResult> GetApiKeys()
    {
        try
        {
            var result = await _apiKeyRepository.GetAllApiKeys(_id);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error"); 
        }
    }

    
    [Route("/api/apikey/add")]
    [HttpPost]
    public async Task<IActionResult> AddApiKey([FromBody] AddApiKeyDto apiKeyData)
    {
        try
        {
            var result = await _apiKeyRepository.AddKey(apiKeyData,_id);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error"); 
        }
    }

    
    [HttpPatch]
    [Route("/api/apikey/edit")]
    public async Task<IActionResult> EditApiKey([FromBody] EditApiKeyDto apiKeyEditData )
    {
        try
        {
            var result = await _apiKeyRepository.EditKey(apiKeyEditData,_id);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error"); 
        }
    }

    [HttpDelete]
    [Route("/api/apikey/delete")]

    public async Task<IActionResult> DeleteApiKey([FromBody] string apiKey)
    {
        try
        {
            var result = await _apiKeyRepository.GetAllApiKeys(_id);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "Internal Server Error"); 
        }
    }
}