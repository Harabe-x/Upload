using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ApiKeyService.Controllers;

/// <summary>
///  A controller that supports other services that need detailed data about an API key  
/// </summary>
[Route("/api/admin/apikey")]
[Controller]
public class AdminApiKeyController : ControllerBase
{
    private readonly IAdminApiKeyRepository _apiKeyRepository;
    private readonly ILogger<AdminApiKeyController> _logger;

    public AdminApiKeyController(IAdminApiKeyRepository apiKeyRepository, ILogger<AdminApiKeyController> logger)
    {
        _apiKeyRepository = apiKeyRepository;
        _logger = logger;
    }

    /// <summary>
    ///  Returns data about any created API Key 
    /// </summary>
    /// <param name="apiKey">API Key</param>
    /// <returns></returns>
    [HttpPost("get")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetApiKey([FromBody] string apiKey)
    {
        try
        {
            var result = await _apiKeyRepository.GetApiKey(apiKey);

            return result != null
                ? Ok(result)
                : BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString()); 
            return BadRequest();
        }
    }
}