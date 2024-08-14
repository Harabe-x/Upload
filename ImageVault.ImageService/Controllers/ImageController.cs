using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ImageService.Controllers;

[Controller] 
[Route("/api/image/")]
public class ImageController : ControllerBase
{
    
    [Authorize]
    [HttpGet("get")]
    public async Task<IActionResult> GetImage()
    {
        return Ok();
    }
}