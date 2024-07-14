using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.RequestMetricsService.Controllers;

[Controller]
[Route("/api/user/metrics")]
public class UserRequestMetricsController : ControllerBase  
{
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserMetrics()
    {
        return Ok();
    }
    
}