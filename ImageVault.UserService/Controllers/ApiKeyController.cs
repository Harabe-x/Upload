using ImageVault.UserService.Data.Dtos.ApiKey;
using ImageVault.UserService.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UserService.Controllers;

[Controller]
[Route("/api/apikey")]
public class ApiKeyController : ControllerBase
{

    private IApiKeyRepository _apiKeyRepository; 

    public ApiKeyController(IApiKeyRepository apiKeyRepository)
    {
        _apiKeyRepository = apiKeyRepository; 
    }
    
    [Authorize]
    [HttpGet]
    [Route("/api/apikey/get")]
    public async Task<IActionResult> GetApiKey()
    {
        return Ok(); 
    }
    
    [Authorize]
    [Route("/api/apikey/add")]
    [HttpPost]
    public async Task<IActionResult> AddApiKey([FromBody] AddApiKeyDto apiKeyData)
    {
        return Ok();
    }

    
    [Authorize]
    [HttpPatch]
    [Route("/api/apikey/edit")]
    public async Task<IActionResult> EditApiKey([FromBody] EditApiKeyDto apiKeyEditData )
    {
        return Ok();        
    }

    [Authorize]
    [HttpDelete]
    [Route("/api/apikey/delete")]

    public async Task<IActionResult> DeleteApiKey([FromBody] string apiKey)
    {
        return Ok();
    }

    
}