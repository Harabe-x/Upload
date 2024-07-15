using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UserService.Controllers;

[Controller]
[Route("/api/apikey")]
public class ApiKeyController : ControllerBase
{
    
    [Authorize]
    [Route("/add")]
    [HttpPost]
    public async Task<IActionResult> AddApiKey()
    {
        return Ok();
    }

    
    [Authorize]
    [HttpPatch]
    [Route("/edit")]
    public async Task<IActionResult> EditApiKey()
    {
        return Ok();        
    }

    [Authorize]
    [HttpDelete]
    [Route("/delete")]

    public async Task<IActionResult> DeleteApiKey()
    {
        return Ok();
    }

    [Authorize]
    [HttpGet]
    [Route("/get")]
    public async Task<IActionResult> GetApiKey()
    {
        return Ok(); 
    }
    
}