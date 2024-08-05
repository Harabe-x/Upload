using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ApiKeyService.Controllers;

[Route("/api/admin/apikey")]
[Controller]
public class AdminApiKeyController : ControllerBase
{

    private readonly IAdminApiKeyRepository _apiKeyRepository; 
    
    public AdminApiKeyController(IAdminApiKeyRepository apiKeyRepository )
    {
        _apiKeyRepository = apiKeyRepository; 
    }
    
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
            return BadRequest();
        }
    }
    

}