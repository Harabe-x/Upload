using ImageVault.ApiKeyService.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ApiKeyService.Controllers;

[Controller]
[Route("/api/apikey")]
public class ApiKeyController : ControllerBase
{
    
    
    [HttpPost]
    [Route("add")]
    [Authorize]
    public async Task<IActionResult> AddKey([FromBody] AddApiKeyDto apiKeyDto)
    {
        return Ok(); 
    }
}